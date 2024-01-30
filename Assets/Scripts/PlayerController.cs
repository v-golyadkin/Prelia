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
    private Vector2 _motionVector;

    private bool _isMoving;

    [Inject]
    private void Construct(CharacterStats stats)
    {
        Speed = stats.Speed;
    }

    private void Awake()
    {
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

        _body.velocity = _motionVector * Speed;
    }

    public bool IsMove()
    {
        return _isMoving;
    }

    public Vector2 GetMotionVector()
    {
        return _motionVector;
    }
}
