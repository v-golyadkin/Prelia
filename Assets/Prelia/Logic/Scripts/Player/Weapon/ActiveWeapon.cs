using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    private MonoBehaviour _currentActiveWeapon;

    private PlayerActions _playerActions;

    private float _timeBetweenAttacks;

    private bool _attackButtonDown, _isAttacking = false;

    public MonoBehaviour CurrentActiveWeapon { get => _currentActiveWeapon; set => _currentActiveWeapon = value; }

    protected override void Awake()
    {
        base.Awake();

        _playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void Start()
    {
        _playerActions.Combat.Attack.started += _ => StartAttacking();
        _playerActions.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }


    private void Update()
    {
        Attack();
    }

    public void WeaponNull()
    {
        _currentActiveWeapon = null;
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        _currentActiveWeapon = newWeapon;

        AttackCooldown();
        _timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    private void Attack()
    {
        if (_attackButtonDown && !_isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (_currentActiveWeapon as IWeapon).Attack();
        }
    }

    private void AttackCooldown()
    {
        _isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttackRoutine());
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private IEnumerator TimeBetweenAttackRoutine()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _isAttacking = false;
    }
}
