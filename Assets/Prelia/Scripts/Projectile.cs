using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;
    [SerializeField] private GameObject _particleOnHitPrefab;
    [SerializeField] private bool _isEnemyProjectile = false;
    [SerializeField] private float _projectileRange = 10f;

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
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if(!collision.isTrigger && (enemyHealth || indestructible || playerHealth))
        {
            if((playerHealth && _isEnemyProjectile) || (enemyHealth && !_isEnemyProjectile))
            {
                playerHealth?.TakeDamage(1, transform);
                Instantiate(_particleOnHitPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if(!collision.isTrigger && indestructible)
            {
                Instantiate(_particleOnHitPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        _projectileRange = projectileRange;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, _startPosition) > _projectileRange)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
}
