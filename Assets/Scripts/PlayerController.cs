using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _playerInputActions;
    public float Speed { get; private set; }
    private Rigidbody2D _body;
    //private Animator _animator;
    private Vector2 _motionVector;

    private bool _isMoving;

    [Inject]
    private void Construct(CharacterStats stats)
    {
        Speed = stats.Speed;
    }

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = _playerInputActions.Gameplay.Movement.ReadValue<Vector2>();

        _motionVector = inputVector.normalized;

        _isMoving = _motionVector.x != 0 || _motionVector.y != 0;

        //PlayerVisual();

        _body.velocity = _motionVector * Speed;
    }

    //private void PlayerVisual()
    //{
    //    _animator.SetBool("isMove", _isMoving);

    //    if (_isMoving)
    //    {
    //        _animator.SetFloat("directionX", _motionVector.x);
    //        _animator.SetFloat("directionY", _motionVector.y);
    //    }
    //}

    public bool IsMove()
    {
        return _isMoving;
    }

    public Vector2 GetMotionVector()
    {
        return _motionVector;
    }
}
