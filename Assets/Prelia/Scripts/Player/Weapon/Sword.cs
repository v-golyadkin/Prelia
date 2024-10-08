using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private Transform _slashSpawnPoint;
    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private float _swordAttackCoolDown = .5f;

    private PlayerActions _playerActions;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private bool _attackButtonDown, _isAttacking = false;

    private GameObject _slash;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerActions = new PlayerActions();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void Start()
    {
        _playerActions.Combat.Attack.started += _ => StartAttacking();
        _playerActions.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowOffset();
        Attack();
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private void Attack()
    {
        if(_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;

            _animator.SetTrigger("Attack");
            _weaponCollider.gameObject.SetActive(true);

            _slash = Instantiate(_slashPrefab, _slashSpawnPoint.position, Quaternion.identity);
            _slash.transform.parent = this.transform.parent;

            StartCoroutine(AttackCoolDownRoutine());
        }

    }

    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(_swordAttackCoolDown);
        _isAttacking = false;
    }

    public void DoneAttackingAnimationEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimationEvent()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if(mousePosition.x < playerScreenPoint.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
