using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool(IS_MOVING, Player.Instance.IsMoving());

        if(Player.Instance.IsMoving())
        {
            animator.SetFloat(MOVE_X, Player.Instance.Direction().x);
            animator.SetFloat(MOVE_Y, Player.Instance.Direction().y);
        }
    }
}
