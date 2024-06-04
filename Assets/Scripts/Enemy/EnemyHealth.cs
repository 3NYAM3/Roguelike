using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHp = 3f;

    private float currentHp;
    private Knockback knockback;
    private Flash flash;

    private void Awake() {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHp = startingHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(currentHp);
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
