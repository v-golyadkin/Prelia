using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecovetyTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _flash = GetComponentInChildren<Flash>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if(enemy && _canTakeDamage)
        {
            TakeDamage(1);
            _knockback.GetKnockedBack(collision.gameObject.transform, _knockBackThrustAmount);
            StartCoroutine(_flash.FlashRoutine());
        }
    }

    private void TakeDamage(int amount)
    {
        _canTakeDamage = false;
        _currentHealth -= amount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecovetyTime);
        _canTakeDamage = true;
    }
}
