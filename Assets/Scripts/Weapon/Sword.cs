using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAniPrefab;
    [SerializeField] private Transform slashAniSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float attackDelay = 0.7f;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAni;
    private DamageSource damageSource;

    private bool canAttack = true;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        damageSource = weaponCollider.GetComponent<DamageSource>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => AttemptAttack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void AttemptAttack()
    {
        if(canAttack) {
            StartCoroutine(Attack());
        }
        
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        damageSource.StartAttack();

        slashAni = Instantiate(slashAniPrefab, slashAniSpawnPoint.position, Quaternion.identity);
        slashAni.transform.parent = this.transform.parent;

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
        damageSource.EndAttack();
    }

    public void DoneAttackingAniEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAniEvent()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAniEvnet()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
