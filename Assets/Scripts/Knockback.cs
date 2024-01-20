using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool gettingKnockBack {  get; private set; }

    [SerializeField] private float knockBackTime = .2f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackTrust)
    {
        gettingKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position) * knockBackTrust * body.mass;
        body.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        body.velocity = Vector2.zero;
        gettingKnockBack = false;
    }
}
