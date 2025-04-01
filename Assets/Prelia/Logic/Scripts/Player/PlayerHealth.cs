using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecovetyTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    protected override void Awake()
    {
        base.Awake();

        _knockback = GetComponent<Knockback>();
        _flash = GetComponentInChildren<Flash>();
    }

    private void Start()
    {
        InitHealthSlider();
    }

    private void InitHealthSlider()
    {
        _healthSlider.maxValue = _config.maxHealth;

        FullHeal();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if(enemy)
        {
            TakeDamage(1, collision.transform);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _config.maxHealth)
        {
            _currentHealth = _config.maxHealth;
        }
           
        UpdateHealthSlider();
    }

    private void FullHeal()
    {
        Heal(_config.maxHealth);
    }

    public void TakeDamage(int amount, Transform hitTransform)
    {
        if(!_canTakeDamage) { return; }

        ScreenShakeManager.Instance.ScreenShake();
        _knockback.GetKnockedBack(hitTransform, _knockBackThrustAmount);
        StartCoroutine(_flash.FlashRoutine());

        _canTakeDamage = false;
        _currentHealth -= amount;
        StartCoroutine(DamageRecoveryRoutine());

        UpdateHealthSlider();
        CheckDeath();
    }

    private void UpdateHealthSlider()
    {
        if(_healthSlider == null)
        {
            _healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        _healthSlider.value = _currentHealth;
    }

    private void CheckDeath()
    {
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Death");
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecovetyTime);
        _canTakeDamage = true;
    }
}
