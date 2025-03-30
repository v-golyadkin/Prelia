using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamingChangeDirection = 2f;
    [SerializeField] private MonoBehaviour _enemyType;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private bool _stopMovingWhileAttacking = false;

    private bool _canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 _roamingPosition;
    private float _timeRoaming = 0f;

    private State _state;
    private EnemyPathfinding _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        _roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch(_state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;
            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming()
    {
        _timeRoaming += Time.deltaTime;

        _enemyPathfinding.MoveTo(_roamingPosition);

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < _attackRange)
        {
            _state = State.Attacking;
        }

        if(_timeRoaming > _roamingChangeDirection)
        {
            _roamingPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > _attackRange)
        {
            _state = State.Roaming;
        }

        if(_attackRange != 0 && _canAttack)
        {
            _canAttack = false;
            (_enemyType as IEnemy).Attack();

            if(_stopMovingWhileAttacking)
            {
                _enemyPathfinding.StopMoving();
            }
            else
            {
                _enemyPathfinding.MoveTo(_roamingPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private Vector2 GetRoamingPosition()
    {
        _timeRoaming = 0;
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
