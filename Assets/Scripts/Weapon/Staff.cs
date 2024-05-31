using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour ,IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicOrbPrefab;
    [SerializeField] private Transform magicOrbSpawnPoint;

    private Animator myAnimator;

    readonly int AttackHash = Animator.StringToHash("Casting");


    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        Debug.Log("Staff Attack");

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        Vector2 direction = mousePos - playerScreenPoint;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        myAnimator.SetTrigger(AttackHash);
        GameObject newMagic;
        if (mousePos.x < playerScreenPoint.x)
        {
            newMagic = Instantiate(magicOrbPrefab, magicOrbSpawnPoint.position, Quaternion.Euler(0, 0, angle + 8.75f));
        }
        else
        {
            newMagic = Instantiate(magicOrbPrefab, magicOrbSpawnPoint.position, Quaternion.Euler(0, 0, angle - 5.8f));
        }
        
        newMagic.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
