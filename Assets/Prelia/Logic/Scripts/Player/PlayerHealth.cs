using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecovetyTime = 1f;

    public bool isDead { get; private set; }

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    const string SPAWN_SCENE_TEXT = "Scene1";

    protected override void Awake()
    {
        base.Awake();

        _knockback = GetComponent<Knockback>();
        _flash = GetComponentInChildren<Flash>();
    }

    private void Start()
    {
        isDead = false;

        InitHealthSlider();
    }

    private void InitHealthSlider()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        _healthSlider.maxValue = _config.maxHealth;

        FullHeal();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

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
        _healthSlider.value = _currentHealth;
    }

    private void CheckDeath()
    {
        if(_currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);

            _currentHealth = 0;
            GetComponentInChildren<Animator>().SetTrigger("death");
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecovetyTime);
        _canTakeDamage = true;
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(SPAWN_SCENE_TEXT);
    }
}
