using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth;

    private int currentHealth;
    private bool canTakeDamege = true;


    private void Awake() {

    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        GameObject enemy = collision.gameObject;

        if (enemy.CompareTag("Enemy")) {


        }
    }
}
