using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charger : MonoBehaviour, IEnemy {

    private EnemyInfo enemyInfo;
    private Rigidbody2D rb;
    private bool ischarging=false;

    public EnemyInfo EnemyInfo {
        private get {
            return enemyInfo;
        }
        set {
            enemyInfo = value;
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack() {
        
        if (!ischarging)
        {
            StartCoroutine(ChargeAttack());
        }
        
    }

    private IEnumerator ChargeAttack() {
        ischarging = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);


        Vector3 direction = (PlayerController.Instance.transform.position-transform.position).normalized;
        float chargeDistance = 4f;
        Vector3 targetPosition = transform.position + direction * chargeDistance;
        float chargeSpeed = enemyInfo.enemySpeed*5;
        rb.velocity = direction*chargeSpeed;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
            yield return null;
        }
        rb.velocity = Vector2.zero;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        ischarging = false;
      
        yield return new WaitForSeconds(enemyInfo.attackDelay);
    }
}
