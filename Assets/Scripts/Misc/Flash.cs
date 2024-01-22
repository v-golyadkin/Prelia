using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float flashTime = .2f;
    [SerializeField] private EnemyHealth enemyHealth;

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    //private EnemyHealth enemyHealth;

    private void Awake()
    {
        //enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material = defaultMaterial;
        enemyHealth.DetectedDeath();
    }
}
