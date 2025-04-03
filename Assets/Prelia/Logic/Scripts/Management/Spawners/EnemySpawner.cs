using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy _enemyPrefab;

    public void SpawnEnemy(Transform target)
    {
        foreach(Transform spawnPoint in spawnPoints)
        {
            Enemy enemy = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.Initialize(target);
        }
    }
}
