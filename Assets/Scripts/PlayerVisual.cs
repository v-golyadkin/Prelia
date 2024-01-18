using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    //private const string MOVE_X = "moveX";
    //private const string MOVE_Y = "moveY";
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        animator.SetBool(IS_MOVING, Player.Instance.IsMoving());
        AdjustPlayerFacingDirection();


        //if(Player.Instance.IsMoving())
        //{
        //    animator.SetFloat(MOVE_X, Player.Instance.Direction().x);
        //    animator.SetFloat(MOVE_Y, Player.Instance.Direction().y);
        //}
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerPosition();

        if (mousePosition.x < playerPosition.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
