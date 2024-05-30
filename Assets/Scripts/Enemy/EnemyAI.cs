using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {

    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        

    }
}
