using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IMoveable
{
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Transform _weaponCollider;

    private PlayerActions _playerActions;
    private Vector2 _moveDirection;
    private Rigidbody2D _rb;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Knockback _knockback;
    private Stamina _stamina;

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
        _knockback = GetComponent<Knockback>();
        _stamina = GetComponent<Stamina>();
    }

    private void Start()
    {
        _playerActions.Combat.Dash.performed += _ => Dash();

        _startingMovespeed = _config.moveSpeed;

        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
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
        if(_knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
        { 
            return; 
        }

        _rb.MovePosition(_rb.position + _moveDirection * (_config.moveSpeed * Time.fixedDeltaTime));
    }

    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
    }

    public void RestoreStamina()
    {
        _stamina.RefreshStamina();
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
        if(!_isDashing && _stamina.CurrentStamina > 0)
        {
            _stamina.UseStamina();
            _isDashing = true;
            _config.moveSpeed *= _config.dashSpeed;
            _trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCoolDown = .25f;
        yield return new WaitForSeconds(dashTime);
        _config.moveSpeed = _startingMovespeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCoolDown);
        _isDashing = false;
    }
}
