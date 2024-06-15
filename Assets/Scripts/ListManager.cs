using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListManager : MonoBehaviour {
    private GameObject infoStage;
    private StarUIManager starUIManager;
    private List<GameObject> stageList;
    private Button[] infoOpenButtons;
    private Button infoCloseButton;
    private Button playButton;
    private bool isInfoOpen = false;
    private int activeStage = 999;



    private void Awake() {
        stageList = new List<GameObject>();
        int k = 1;
        while (true) {
            GameObject stage = GameObject.Find("Stage" + k);
            if (stage != null) {
                stageList.Add(stage);
            } else {
                break;
            }
            Debug.Log(stageList[k - 1]);
            k++;
        }
        GameObject info = GameObject.Find("InfoStage");
        infoStage = info;
        infoStage.SetActive(false);

        infoOpenButtons = new Button[stageList.Count];
        isInfoOpen = false;

        for (int i = 0; i < stageList.Count; i++) {
            infoOpenButtons[i] = stageList[i].transform.GetChild(0).GetComponent<Button>();
            if (i != 0) {
                stageList[i].SetActive(false);
            }

            int index = i;
            infoOpenButtons[i].onClick.AddListener(() => OpenInfoStage(index));
        }

        GetComponent<StarManagement>().InitializeMaps(GetStageCount());
        starUIManager = GetComponent<StarUIManager>();
        if (starUIManager != null) {
            starUIManager.UpdateAllStars();
        } else {
            Debug.LogError("StarUIManager 컴포넌트를 찾을 수 없습니다.");
        }
        starUIManager.UpdateAllStars();

    }
    private void OnEnable() {
        Debug.Log(isInfoOpen+"sadasdasdasda");

        infoCloseButton = infoStage.transform.GetChild(0).GetComponent<Button>();
        playButton = infoStage.transform.GetChild(1).GetChild(0).GetComponent<Button>();

        infoCloseButton.onClick.AddListener(CloseInfoStage);
        playButton.onClick.AddListener(LoadStage);


        for(int i = 0;i < stageList.Count-1;i++) {
            if(PlayerPrefs.GetInt("MapStars" + i,0) > 0) {
                stageList[i+1].SetActive(true);
            }
        }
    }

    private void OnDisable() {
        if (infoCloseButton != null) {
            infoCloseButton.onClick.RemoveListener(CloseInfoStage);
        }
        if (playButton != null) {
            playButton.onClick.RemoveListener(LoadStage);
        }
    }

    private void Update() {
        int i = 0;
        for (i = 0; i < stageList.Count; i++) {
            if (i != 0) {
                int starNum = GetComponent<StarManagement>().GetStarsForMap(i);
                if (starNum >= 1 && !stageList[i].activeSelf) {
                    stageList[i].SetActive(true);
                }
            }
        }
    }


    public void OpenInfoStage(int stageIndex) {
        Debug.Log("OpenInfoStage" + activeStage + isInfoOpen);
        if (!isInfoOpen) {
            infoStage.SetActive(true);
            isInfoOpen = true;
        } else {
            infoStage.SetActive(false);
            isInfoOpen = false;
        }

        activeStage = stageIndex;
    }

    public void CloseInfoStage() {
        Debug.Log("OpenInfoStage no param" + activeStage + isInfoOpen);
        if (!isInfoOpen) {
            infoStage.SetActive(true);
            isInfoOpen = true;
        } else {
            infoStage.SetActive(false);
            isInfoOpen = false;
        }
    }

    public void LoadStage() {
        Debug.Log("LoadStage" + activeStage);

        infoStage.SetActive(false);
        isInfoOpen = false;
        SceneManager.LoadScene("Stage" + (activeStage + 1));
    }

    public void BackTitle() {
        SceneManager.LoadScene("TitleMenu");
    }

    public int GetStageCount() {
        return stageList.Count;
    }
}
