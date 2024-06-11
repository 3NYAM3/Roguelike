using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private float moveSpeed = 7f;

    private EnemyInfo enemyInfo;
    private Vector3 startPosition;
    private Vector3 direction;


    private void Awake() {
        
    }

    private void Start() {
        startPosition = transform.position;
        direction = (PlayerController.Instance.transform.position - transform.position).normalized;
    }

    private void Update() {
        MoveProjectile();
        DetectFireDistance();
    }
    internal void UpdateEnemyInfo(EnemyInfo enemyInfo) {
        this.enemyInfo = enemyInfo;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();


        if (!collision.isTrigger && (player || indestructible)) {
            player?.TakeDamage(enemyInfo.enemyDamage, transform);
            GameObject vfx = Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(vfx, 1f);
            Destroy(gameObject);
        }
    }

    private void MoveProjectile() {
        
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    private void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPosition) > enemyInfo.attackRange) {
            Destroy(gameObject);
        }
    }

    
}
