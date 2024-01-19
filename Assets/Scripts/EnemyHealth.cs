using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
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
