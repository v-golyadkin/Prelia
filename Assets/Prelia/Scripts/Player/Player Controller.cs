using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IMoveable
{
    [SerializeField] private PlayerConfig PlayerConfig;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Transform _weaponCollider;

    private PlayerActions _playerActions;
    private Vector2 _moveDirection;
    private Rigidbody2D _rb;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _facingLeft = false;
    private bool _isDashing = false;

    private float _startingMovespeed;

    public bool FacingLeft { get => _facingLeft; }

    protected override void Awake()
    {
        base.Awake();

        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody2D>();       
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _playerActions.Combat.Dash.performed += _ => Dash();

        _startingMovespeed = PlayerConfig.MoveSpeed;
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

    public void Move()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (PlayerConfig.MoveSpeed * Time.fixedDeltaTime));
    }

    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePosition.x < playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
            _facingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            _facingLeft = false;
        }
    }

    private void PlayerInput()
    {
        _moveDirection = _playerActions.Movement.Move.ReadValue<Vector2>();

        _animator.SetFloat("moveX", _moveDirection.x);
        _animator.SetFloat("moveY", _moveDirection.y);
    }


    private void Dash()
    {
        if(!_isDashing)
        {
            _isDashing = true;
            PlayerConfig.MoveSpeed *= PlayerConfig.DashSpeed;
            _trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCoolDown = .25f;
        yield return new WaitForSeconds(dashTime);
        PlayerConfig.MoveSpeed = _startingMovespeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCoolDown);
        _isDashing = false;
    }
}
