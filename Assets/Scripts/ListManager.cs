using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListManager : MonoBehaviour
{
    public void LoadStage1() {
        SceneManager.LoadScene("MapScene1");
    }
}
