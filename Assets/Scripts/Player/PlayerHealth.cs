using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float knockBackAmount = 10f;
    [SerializeField] private float damageRecovoryTime = 1f;

    private Slider HPSlider;
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

        UpdateHPSlider();
    }

    public float getHpPercent() {
        return currentHealth / maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        if (enemy) {
            Debug.Log(enemy + "�� �÷��̾�� " + enemy.enemyInfo.enemyDamage + "�������� �������ϴ�. ");
        }
        if (other.gameObject.CompareTag("Enemy") && canTakeDamege) {
            if(enemy.enemyInfo.attackType == AttackType.Boomer) {
                return;
            }
            TakeDamage(enemy.enemyInfo.enemyDamage, other.transform);
        }
    }

   public void HpOrbGet() {
        if (currentHealth < maxHealth) {
            currentHealth += 10;
            UpdateHPSlider();
        }
    }

    public void TakeDamage(float damage, Transform other) {
        if (!canTakeDamege) {
            if(other.GetComponent<EnemyAI>().enemyInfo.attackType != AttackType.Boomer) {
                return ;
            }
        }

        knockback.GetKnockedBack(other.gameObject.transform, knockBackAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamege = false;
        currentHealth -= damage;
        Debug.Log("���� �÷��̾��� ü�� : "+currentHealth);
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHPSlider();
        DetectDeath();
    }
    private void DetectDeath() {
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("�÷��̾� ���~~~~~~");
            //Destroy(gameObject);
        }
    }
    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecovoryTime);
        canTakeDamege = true;
    }

    private void UpdateHPSlider() {
        if(HPSlider == null) {
            HPSlider = GameObject.Find("HPbar").GetComponent<Slider>();
        }
        HPSlider.maxValue = maxHealth;
        HPSlider.value = currentHealth;
    }
}