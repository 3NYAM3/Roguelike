using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHp = 3f;

    private float currentHp;

    private void Start()
    {
        currentHp = startingHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(currentHp);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
