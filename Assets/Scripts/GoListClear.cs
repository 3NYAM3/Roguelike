using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoListClear : MonoBehaviour {
    public void GoList() {
        PlayerController.DestroyInstance();
        SceneManager.LoadScene("MapList");
    }
}
