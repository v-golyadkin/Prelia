using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;
    [SerializeField] private GameObject _particleOnHitPrefab;

    private WeaponConfig _weaponConfig;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();

        if(!collision.isTrigger && (enemyHealth || indestructible))
        {
            Instantiate(_particleOnHitPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void UpdateWeaponInfo(WeaponConfig weaponConfig)
    {
        _weaponConfig = weaponConfig;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, _startPosition) > _weaponConfig.weaponRange)
        {
            Destroy(gameObject);
        }
    }
}
