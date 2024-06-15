using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManagement : MonoBehaviour
{
    
    [SerializeField] private GameObject qgQ;

    private bool qgQisOpen = false;

    public void StartButton() {
        SceneManager.LoadScene("MapList");
    }

    public void manualButton() {
        SceneManager.LoadScene("ManualScene");
    }

    public void QuitGameQ() {
        if (!qgQisOpen) {
            qgQ.SetActive(true);
            qgQisOpen = true;
        } else {
            qgQ.SetActive(false);
            qgQisOpen = false;
        }
    }
    public void QuitGame() {
        Application.Quit();
    }
}
