using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;

    public event EventHandler OnPlayerDash;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_started; ;
        playerInputActions.Combat.Dash.started += PlayerDash_started;
    }

    private void PlayerDash_started(InputAction.CallbackContext context)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Movement.Move.ReadValue<Vector2>();

        return inputVector;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }
}