using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : MonoBehaviour, IEnemy {
    private EnemyInfo enemyInfo;
    private Animator boomAnimator;
    private GameObject explosionIndicator;
    public EnemyInfo EnemyInfo {
        private get {
            return enemyInfo;
        }
        set {
            enemyInfo = value;
        }
    }
    private void Awake() {
        Animator boomAnimator = GetComponent<Animator>();
        explosionIndicator = transform.GetChild(0).gameObject;
        explosionIndicator.SetActive(false);
    }

    public void Attack() {
        StartCoroutine(BoomerAttack());
    }

    private IEnumerator BoomerAttack() {
        explosionIndicator.SetActive(true);

        boomAnimator.SetTrigger("PrepareExplosion");
        yield return new WaitForSeconds(2.0f);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyInfo.explosionRange);
        foreach(var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player")) {
                Debug.Log("플레이어에게" + enemyInfo.enemyDamage + "데미지를 줌");
            }
        }

        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.explosionRange);
    }

}
