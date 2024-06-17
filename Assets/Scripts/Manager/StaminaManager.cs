using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour {
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite full, empty;
    
    private int staminaChargeDelay = 2;
    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;

    private void Awake() {
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start() {
        staminaContainer = GameObject.Find("Stamina Container").transform;
    }

    public void UseStamina() {
        CurrentStamina--;
        UpdateStaminaImages();
    }

    public void RefreshStamina() {
        if (CurrentStamina < maxStamina) {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    private IEnumerator RefreshStaminaRoutine() {
        while (true) {
            yield return new WaitForSeconds(staminaChargeDelay);
            RefreshStamina();
        }
    }

    private void UpdateStaminaImages() {
        for (int i = 0; i < maxStamina; i++) {
            if (i <= CurrentStamina - 1) {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = full;
            } else {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = empty;
            }
        }

        if (CurrentStamina < maxStamina) {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
