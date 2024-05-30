using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Pistol Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
