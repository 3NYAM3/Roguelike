using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth=150;
    [SerializeField] private float knockBackAmount = 5f;

    private int currentHealth;
    private bool canTakeDamege = true;
    private Knockback knockback;
    //private Flash flash;


    private void Awake() {

    }

    private void Start() {
        currentHealth = maxHealth;
    }
    public void TackDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 10f);
        DetectDeath();
    }
    private void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        GameObject enemy = collision.gameObject;

        if (enemy.CompareTag("Enemy")) {


        }
    }
}
