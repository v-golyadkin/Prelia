using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;

    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();    
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        knockback.GetKnockedBack(Player.Instance.transform, 15f);
        DetectedDeath();
    }

    private void DetectedDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
