using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListManager : MonoBehaviour
{
    [SerializeField] List<GameObject> stageList;

    private List<bool> isInfoOpen;

    private void Awake() {
        foreach (var stage in stageList) {
            stage.SetActive(false);
        }
        isInfoOpen = new List<bool>();

        for(int i = 0; i < stageList.Count; i++) {
            isInfoOpen.Add(false);
        }

        StarManagement.Instance.InitializeMaps(GetStageCount());
    }

    public void InfoStage1() {
        if (!isInfoOpen[0]) {
            stageList[0].SetActive(true);
            isInfoOpen[0] = true;
        } else {
            stageList[0].SetActive(false);
            isInfoOpen[0] = false;
        }
    }
    
    public void LoadStage1() {
        SceneManager.LoadScene("Stage1");
    }

    public int GetStageCount() {
        return stageList.Count;
    }
}
