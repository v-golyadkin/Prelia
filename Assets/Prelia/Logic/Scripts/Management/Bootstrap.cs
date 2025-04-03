using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Awake()
    {
        var player = _playerSpawner.Spawn();
        _enemySpawner.SpawnEnemy(player.transform);
    }
}
