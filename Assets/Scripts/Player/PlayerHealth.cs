using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 150000;
    [SerializeField] private float knockBackAmount = 10f;
    [SerializeField] private float damageRecovoryTime = 1f;

    private float currentHealth;
    private bool canTakeDamege = true;
    private Knockback knockback;
    private Flash flash;


    private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();

    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        Debug.Log(enemy+"�� �÷��̾�� "+enemy.enemyInfo.enemyDamage+"�������� �������ϴ�. ");
        if (other.gameObject.CompareTag("Enemy") && canTakeDamege) {
            TakeDamage(enemy.enemyInfo.enemyDamage, other.transform);
        }
    }
    public void TakeDamage(float damage, Transform other) {
        if(!canTakeDamege) { return; }

        knockback.GetKnockedBack(other.gameObject.transform, knockBackAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamege = false;
        currentHealth -= damage;
        Debug.Log("���� �÷��̾��� ü�� : "+currentHealth);
        DetectDeath();
        StartCoroutine(DamageRecoveryRoutine());
    }
    private void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecovoryTime);
        canTakeDamege = true;
    }
}