using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy {

    [SerializeField] private GameObject bulletPrefab;
    private EnemyInfo enemyInfo;

    public EnemyInfo EnemyInfo {
        private get {
            return enemyInfo;
        }
        set {
            enemyInfo = value;
        }
    }

    public void Attack() {
        Vector2 targetDirction = PlayerController.Instance.transform.position-transform.position;
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<EnemyProjectile>().UpdateEnemyInfo(enemyInfo);
    }

}
