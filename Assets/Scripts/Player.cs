using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    private Vector2 directionVector;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHandler();
    }


    private void MoveHandler()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        body.velocity = inputVector * moveSpeed;
    }


}
