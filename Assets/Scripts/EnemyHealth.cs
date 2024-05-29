using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHp = 3;

    private int currentHp;

    private void Start()
    {
        currentHp = startingHp;
    }

    public void TakeDamage(int damage)
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
