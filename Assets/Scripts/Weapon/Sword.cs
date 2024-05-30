using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAniPrefab;
    [SerializeField] private Transform slashAniSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float attackDelay = .7f;

    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAni;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAni = Instantiate(slashAniPrefab, slashAniSpawnPoint.position, Quaternion.identity);
        slashAni.transform.parent = this.transform.parent;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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
