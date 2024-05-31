using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject gunFlame;

    private bool activeFlame = false;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;

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
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newBullet.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        Vector2 direction = mousePos - playerScreenPoint;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Flame()
    {
        if (!activeFlame)
        {
            gunFlame.SetActive(true);
            activeFlame = true;
        }
        else
        {
            gunFlame.SetActive(false);
            activeFlame = false;
        }
    }
}
