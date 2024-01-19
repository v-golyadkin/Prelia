using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    private const string ATTACK = "Attack";

    [SerializeField] private Sword sword;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
    private void Update()
    {
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerPosition();

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (mousePosition.x < playerPosition.x)
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        else
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
