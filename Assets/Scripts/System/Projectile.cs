using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;

    private EnemyInfo enemyInfo;
    private WeaponInfo weaponInfo;
    private Vector3 startPosition;
    private Transform target;
    private MagicOrb magicOrb;
    

    private void Awake() {
       // enemyInfo = GetComponent<EnemyInfo>();
    }
    private void Start()
    {
        startPosition = transform.position;
        magicOrb = GetComponent<MagicOrb>();
    }

    private void Update()
    {
        if (weaponInfo.isHoming)
        {
            MoveProjectile();
            FindTarget();
            MoveToTarget();
        }
        else
        {
            MoveProjectile();
        }
        
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth  = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();


        if(!collision.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile)|| (enemyHealth && !isEnemyProjectile)) {
                player?.TakeDamage(enemyInfo.enemyDamage, transform);
                GameObject vfx=Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(vfx,1f);
                Destroy(gameObject);
            } else if (!collision.isTrigger && indestructible) {
                GameObject vfx = Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(vfx, 1f);
                Destroy(gameObject);
            }
            
            
        }
    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }



    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime* moveSpeed);
    }

    private void FindTarget()
    {
        float detectionRange = magicOrb != null ? magicOrb.DetectionRange : 0;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        if(enemies.Length == 0)
        {
            target = null;
            return;
        }

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach(Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.transform;
                }
            }
            
        }

        target = nearestEnemy;
    }

    private void MoveToTarget()
    {
        if(target == null) { return; }

        Vector3 direction=(target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
}
