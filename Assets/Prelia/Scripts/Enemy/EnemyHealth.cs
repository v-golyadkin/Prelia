using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private GameObject _deathVFXPrefab;
    [SerializeField] private float _knockBackThrust = 15f;

    private int _currentHealth;
    private Knockback _knockback;
    private Flash _flashEffect;

    private void Awake()
    {
        _flashEffect = GetComponentInChildren<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController.instance.transform, _knockBackThrust);
        StartCoroutine(_flashEffect.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(_flashEffect.GetRestoreMaterialTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }            
    }
}
