using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    
    private PlayerControls playercontrols;

    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        playercontrols = new PlayerControls();
    }

    private void OnEnable()
    {
        playercontrols.Enable();
    }

    private void Start()
    {
        playercontrols.Combat.Attack.started += _ => StartAttacking();
        playercontrols.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking= true;
            (currentActiveWeapon as IWeapon).Attack();
        }
    }
}
