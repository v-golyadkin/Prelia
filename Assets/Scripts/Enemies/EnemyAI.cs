using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(Roaming());
    }

    private IEnumerator Roaming()
    {
        while(state == State.Roaming)
        {
            Vector2 roamingVector = GetRoamingVector();
            enemyPathfinding.MoveTo(roamingVector);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingVector()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}