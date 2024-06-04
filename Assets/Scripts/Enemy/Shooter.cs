using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy {

    private EnemyInfo enemyInfo;

    public EnemyInfo EnemyInfo {
        private get {
            return enemyInfo;
        }
        set {
            enemyInfo = value;
        }
    }

    public void Attack() {
        Debug.Log("shooter Attack");
    }

}