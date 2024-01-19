using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    private Rigidbody2D body;
    private Vector2 directionVector;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body.velocity = moveSpeed * directionVector;
    }


    public void MoveTo(Vector2 target)
    {
        directionVector = target;
    }
}
