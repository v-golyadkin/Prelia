using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private Visual visual;

    private float currentHealth;
    private Knockback knockback;
    //private Flash flash;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();    
        //flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(Player.Instance.transform, 10f);
        StartCoroutine(visual.GetFlash().FlashRoutine());
    }

    public void DetectedDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
