using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 4f;

    private Vector2 directionVector;
    private Rigidbody2D body;

    private bool isMoving = false;

    private void Awake()
    {
        Instance = this;

        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveHandler();
    }


    private void MoveHandler()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        directionVector = inputVector.normalized;

        isMoving = directionVector.x != 0 || directionVector.y != 0;

        Debug.Log(isMoving + " " + directionVector.x + " " + directionVector.y);

        body.velocity = directionVector * moveSpeed;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public Vector2 Direction()
    {
        return directionVector;
    }

}
