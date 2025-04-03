using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : Singleton<Player>
{
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    protected override void Awake()
    {
        base.Awake();

        _playerController = GetComponent<PlayerController>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void Heal() => _playerHealth.Heal(1);
    public void Heal(int amount) => _playerHealth.Heal(amount);
    public void RestoreStamina() => _playerController.RestoreStamina();

    public bool isDead => _playerHealth.isDead;
}
