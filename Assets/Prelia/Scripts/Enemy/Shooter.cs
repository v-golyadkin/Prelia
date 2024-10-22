using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletMoveSpeed = 2f;
    [SerializeField] private int _burstCount;
    [SerializeField] private int _projectilePerBurst;
    [SerializeField][Range(0, 359)] private float _angleSpread;
    [SerializeField] private float _startingDistance = 0.1f;
    [SerializeField] private float _timeBetweenBursts;
    [SerializeField] private float _restTime = 1f;
    [SerializeField] private bool _stagger;
    [SerializeField] private bool _oscillate;

    private bool _isShooting = false;

    private void OnValidate()
    {
        if(_oscillate) { _stagger = true; }
        if(!_oscillate) { _stagger = false; }
        if(_projectilePerBurst < 1) { _projectilePerBurst = 1; }
        if(_burstCount < 1) { _burstCount = 1; }
        if(_startingDistance < 0.1f) { _startingDistance = 0.1f; }
        if(_timeBetweenBursts < 0.1f) { _timeBetweenBursts = 0.1f; }
        if(_restTime < 0.1f) { _restTime = 0.1f; }
        if(_angleSpread == 0) { _projectilePerBurst = 1; }
        if(_bulletMoveSpeed <= 0) { _bulletMoveSpeed = 0.1f; }
    }

    public void Attack()
    {
        if(!_isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        _isShooting = true;

        float startAngle, currenctAngle, stepAngle, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetCloneOfInfluence(out startAngle, out currenctAngle, out stepAngle, out endAngle);

        if(_stagger) { timeBetweenProjectiles = _timeBetweenBursts / _projectilePerBurst; }

        for(int i = 0; i < _burstCount; i++)
        {
            if (!_oscillate)
            {
                TargetCloneOfInfluence(out startAngle, out currenctAngle, out stepAngle, out endAngle);
            }

            if(_oscillate && i % 2 != 1)
            {
                TargetCloneOfInfluence(out startAngle, out currenctAngle, out stepAngle, out endAngle);
            }
            else if (_oscillate) 
            {
                currenctAngle = endAngle;
                endAngle = startAngle;
                startAngle = currenctAngle;
                stepAngle *= -1;
            }

            for(int j = 0; j < _projectilePerBurst; j++)
            {
                Vector2 spawnPosition = FindBulletSpawnPosition(currenctAngle);

                GameObject newBullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity   );
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(_bulletMoveSpeed);
                }

                currenctAngle += stepAngle;

                if (_stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currenctAngle = startAngle;
            
            if(!_stagger) { yield return new WaitForSeconds(_timeBetweenBursts); }
        }

        yield return new WaitForSeconds(_restTime);
        _isShooting = false;
    }

    private Vector2 FindBulletSpawnPosition(float currenctAngle)
    {
        float x = transform.position.x + _startingDistance * Mathf.Cos(currenctAngle * Mathf.Deg2Rad);
        float y = transform.position.y + _startingDistance * Mathf.Sin(currenctAngle * Mathf.Deg2Rad);

        Vector2 position = new Vector2(x, y);

        return position;
    }

    private void TargetCloneOfInfluence(out float startAngle, out float currenctAngle, out float stepAngle, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngel = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngel;
        endAngle = targetAngel;
        float endAngel = targetAngel;
        currenctAngle = targetAngel;
        float halfAngelSpread = 0f;
        stepAngle = 0f;

        if(_angleSpread != 0f)
        {
            stepAngle = _angleSpread / (_projectilePerBurst - 1);
            halfAngelSpread = _angleSpread / 2f;
            startAngle = targetAngel - halfAngelSpread;
            endAngel = targetAngel + halfAngelSpread;
            currenctAngle = startAngle;
        }
    }
}
