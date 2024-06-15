using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualManagement : MonoBehaviour
{
    public void BackToTitle() {
        SceneManager.LoadScene("TitleMenu");
    }
}
