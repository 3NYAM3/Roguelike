using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndex = 0;
    private int totalSlots;
    private const float scrollThreshold = 0.1f;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.KeyBoard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        playerControls.Inventory.Scroll.performed += ctx => ScrollActiveSlot(ctx.ReadValue<Vector2>());
        totalSlots = this.transform.childCount;
        UpdateActiveHighlight();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void ToggleActiveSlot(int value)
    {
        ToggleActiveHighlight(value - 1);
    }

    private void ScrollActiveSlot(Vector2 value)
    {
        if (value.y<-scrollThreshold)
        {
            activeSlotIndex = (activeSlotIndex + 1) % totalSlots;
            UpdateActiveHighlight();
        }
        else if (value.y>scrollThreshold)
        {
            activeSlotIndex = (activeSlotIndex - 1+totalSlots) % totalSlots;
            UpdateActiveHighlight();
        }
        
    }

    private void ToggleActiveHighlight(int index)
    {
        activeSlotIndex = index;

        UpdateActiveHighlight();
    }

    private void UpdateActiveHighlight()
    {
        for(int i = 0; i < totalSlots; i++)
        {
            this.transform.GetChild(i).GetChild(0).gameObject.SetActive(i==activeSlotIndex);
        }
        ChangeActiveWeapon();

    }

    private void ChangeActiveWeapon()
    {
        Debug.Log(transform.GetChild(activeSlotIndex).GetComponent<InventorySlot>().GetWeaponInfo().weaponPrefab.name);
    }
}
