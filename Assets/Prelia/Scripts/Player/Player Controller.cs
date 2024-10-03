using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig PlayerConfig;

    private PlayerActions _playerActions;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePosition.x < playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void PlayerInput()
    {
        _movement = _playerActions.Movement.Move.ReadValue<Vector2>();

        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (PlayerConfig.MoveSpeed * Time.fixedDeltaTime));
    }
}
