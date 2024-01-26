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
        Vector2 inputVector = _playerInputActions.Gameplay.Movement.ReadValue<Vector2>().normalized;

        _body.velocity = inputVector * Speed;
    }
}
