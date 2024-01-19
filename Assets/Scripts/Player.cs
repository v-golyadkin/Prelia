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

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;    
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

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

    public Vector3 GetPlayerPosition()
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerPosition;
    }
}
