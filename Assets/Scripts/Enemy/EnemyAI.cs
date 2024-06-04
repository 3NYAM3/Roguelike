using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private PolygonCollider2D room;
    [SerializeField] private EnemyInfo enemyInfo;




    private enum State {
        Standby,
        Move,
        Attacking
    }

    private Rigidbody2D rb;
    private Animator animator;
    private State state;
    private IEnemy enemyAttack;
    private float distanceToPlayer;
    private float ShooterFleeRange;
    private Knockback knockback;



    private void Awake() {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        switch (enemyInfo.attackType) {
            /*case AttackType.Charger:
                Charger charger = gameObject.GetComponent<Charger>();
                charger.EnemyInfo = enemyInfo;
                enemyAttack = charger;
                break;*/
            case AttackType.Shooter:
                Shooter shooter = gameObject.GetComponent<Shooter>();
                shooter.EnemyInfo = enemyInfo;
                enemyAttack = shooter;
                break;
            case AttackType.Boomer:
                Boomer boomer = gameObject.GetComponent<Boomer>();
                boomer.EnemyInfo = enemyInfo;
                enemyAttack = boomer;
                break;
            case AttackType.Mover:
                Mover mover = gameObject.GetComponent<Mover>();
                mover.EnemyInfo = enemyInfo;
                enemyAttack = mover;
                break;
            default:
                break;

        }

        state = State.Standby;
    }

    private void Update() {
        if (knockback.GettingKnockedBack) { return; }
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        MovementStateControl();
        LookAtPlayer();
    }
   



    private void MovementStateControl() {
        switch (state) {
            default:
            case State.Standby:
                StandbyState();
                break;
            case State.Move:
                StopAllCoroutines();
                animator.SetTrigger("Moving");
                Moving();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void StandbyState() {
        if (IsPlayerInRoom()) {
            state = State.Move;
        }
    }

    private bool IsPlayerInRoom() {
        return room.OverlapPoint(PlayerController.Instance.transform.position);
    }


    private void Moving() {

        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.velocity = direction * enemyInfo.enemySpeed;

        if (enemyInfo.attackType == AttackType.Shooter) {
            if (distanceToPlayer <= ShooterFleeRange) {
                direction = (transform.position - PlayerController.Instance.transform.position).normalized;
                rb.velocity = direction * enemyInfo.enemySpeed;
            } else if (distanceToPlayer > enemyInfo.attackRange - 3) {
                rb.velocity = direction * enemyInfo.enemySpeed;
                if (distanceToPlayer > enemyInfo.attackRange) {
                    state = State.Attacking;
                }
            }

        } else if (enemyInfo.attackType == AttackType.Boomer) {
            if (distanceToPlayer > enemyInfo.attackRange) {
                rb.velocity = direction * enemyInfo.enemySpeed;
            } else {
                state = State.Attacking;
            }

        /*} else if (enemyInfo.attackType == AttackType.Charger) {
            state = State.Attacking;*/
        } else if (enemyInfo.attackType == AttackType.Mover) {
            rb.velocity = direction * enemyInfo.enemySpeed;
        }
    }
    private void LookAtPlayer() {

        Vector3 scale = transform.localScale;
        if (PlayerController.Instance.transform.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x);
        } else {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

    }

    private void Attacking() {
        enemyAttack.Attack();
    }

    private void SetMoving() {
        state = State.Move;
    }




}
