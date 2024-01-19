using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;

    public event EventHandler OnSwordSwing;

    public void Attack()
    {
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerPosition();

        if (mousePosition.x < playerPosition.x)
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
