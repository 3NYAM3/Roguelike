using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagement : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenuUI;
    [SerializeField] private GameObject btslQ;
    [SerializeField] private GameObject qgQ;

    private bool isPaused = false;
    private bool btslQisOpen = false;
    private bool qgQisOpen = false;


    private void Awake() {
        pauseMenuUI.SetActive(false);  
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }
    public void BackToStageListQ() {
        if (!btslQisOpen) {
            btslQ.SetActive(true);
            btslQisOpen = true;
        }else {
            btslQ.SetActive(false);
            btslQisOpen = false;
        }
        
    }


    public void BackToStageList() {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MapList"); 
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
