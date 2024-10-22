using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour, IMoveable
{
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Knockback _knockback;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(_knockback.GettingKnockedBack) { return; }

        Move();

        if(_moveDirection.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void Move()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = targetPosition;
    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }
}
