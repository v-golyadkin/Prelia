using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _slashPrefab;
    [SerializeField] private Transform _slashSpawnPoint;
    [SerializeField] private WeaponConfig _weaponConfig;

    private Transform _weaponCollider;
    private Animator _animator;
    private GameObject _slash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weaponCollider = PlayerController.Instance.GetWeaponCollider();
        _slashSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Update()
    {
        MouseFollowOffset();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _weaponCollider.gameObject.SetActive(true);

        _slash = Instantiate(_slashPrefab, _slashSpawnPoint.position, Quaternion.identity);
        _slash.transform.parent = this.transform.parent;

    }

    public void DoneAttackingAnimationEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimationEvent()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public WeaponConfig GetWeaponInfo()
    {
        return _weaponConfig;
    }

    private void MouseFollowOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if(mousePosition.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
