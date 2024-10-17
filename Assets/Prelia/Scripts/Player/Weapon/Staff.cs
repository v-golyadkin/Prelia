using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private GameObject _magicLaser;
    [SerializeField] private Transform _magicLaserSpawnPoint;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }


    public void Attack()
    {
        _animator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffProjectileAnimationEvent()
    {
        GameObject newLaser = Instantiate(_magicLaser, _magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(_weaponConfig.weaponRange);
    }

    public WeaponConfig GetWeaponInfo()
    {
        return _weaponConfig;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
