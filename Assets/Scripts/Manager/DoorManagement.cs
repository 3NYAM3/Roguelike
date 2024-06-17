using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagement : MonoBehaviour {
    [SerializeField] private List<GameObject> btns;
    [SerializeField] private GameObject doorVFX;

    private bool isAllBtnsPressed = false;

    private void Update() {
        isAllBtnsPressed = CheckBtnsPressed();
        if (isAllBtnsPressed) {
            OpenDoor();
        }
    }

    private bool CheckBtnsPressed() {
        foreach (GameObject btn in btns) {
            if (btn != null) {
                if (!btn.gameObject.GetComponent<ButtonManager>().IsPressed) {
                    return false;
                }
            }
        }
        return true;
    }

    private void OpenDoor() {
        GameObject vfx = Instantiate(doorVFX, transform.position, transform.rotation);
        Destroy(vfx, 1f);
        Destroy(gameObject);
    }
}
