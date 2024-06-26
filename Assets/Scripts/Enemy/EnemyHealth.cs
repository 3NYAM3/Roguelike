using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHp = 3f;
    [SerializeField] private GameObject deathVFXPrefab;


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
        StartCoroutine(CheckDetectDeathRoutine());
    }
    private IEnumerator CheckDetectDeathRoutine() {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHp <= 0)
        {
            GameObject vfx = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx,1f);
            Destroy(gameObject);
        }
    }
}
