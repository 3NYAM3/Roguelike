using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {
    Shooter,
    Boomer,
    Mover
    
}

[CreateAssetMenu(menuName = "NewEnemy")]
public class EnemyInfo : ScriptableObject {
    public GameObject enemyPrefab;
    public float enemyDamage;
    public float enemySpeed;
    public AttackType attackType;
    public float attackDelay;
    public float attackRange;
    public float explosionRange;
}

