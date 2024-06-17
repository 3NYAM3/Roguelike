using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    
    public void Retry() {
        Time.timeScale = 1f;
        PlayerController.DestroyInstance();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Stage() {
        Time.timeScale = 1f;
        PlayerController.DestroyInstance();
        SceneManager.LoadScene("MapList");
    }
}
