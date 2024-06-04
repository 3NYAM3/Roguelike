using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : MonoBehaviour, IEnemy {
    [SerializeField] private GameObject explosionVFX;

    private EnemyInfo enemyInfo;
    private Animator boomAnimator;
    private GameObject explosionWarn;
    private Rigidbody2D rb;
    private Flash flash;
    
    public EnemyInfo EnemyInfo {
        private get {
            return enemyInfo;
        }
        set {
            enemyInfo = value;
        }
    }
    private void Awake() {
        flash =GetComponent<Flash>();
        rb = GetComponent<Rigidbody2D>();
        boomAnimator = GetComponent<Animator>();
        explosionWarn = transform.GetChild(0).gameObject;
        explosionWarn.SetActive(false);
    }

    public void Attack() {
        rb.velocity = Vector3.zero;
        boomAnimator.SetTrigger("PrepareExplosion");
    }

    private void BoomerAttack() {
        flash.StartedExplosion = true;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyInfo.explosionRange);
        foreach(var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player")) {
                Debug.Log("�÷��̾��" + enemyInfo.enemyDamage + "�������� ��");
            }
        }

        GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
        Destroy(vfx, 1f);

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.explosionRange);
    }

}