using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIManager : MonoBehaviour {

    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite filledStar;


    private List<GameObject> stageList;

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
            Debug.Log(stageList[k - 1] + "sdfsdf");
            k++;
        }
    }
    public void UpdateAllStars() {
        for (int i = 0; i < stageList.Count; i++) {
            UpdateStars(i);
        }
    }

    public void UpdateStars(int mapIndex) {
        if (mapIndex >= stageList.Count || mapIndex < 0) {
            Debug.LogError("맵 인덱스가 잘못되었습니다.");
            return;
        }
        int starCount = GetComponent<StarManagement>().GetStarsForMap(mapIndex);
        Transform starGroup = stageList[mapIndex].transform.Find("Stars");

        if (starGroup != null) {
            for (int i = 0; i < starGroup.childCount; i++) {
                Image starImage = starGroup.GetChild(i).GetComponent<Image>();
                if (starImage != null) {
                    starImage.sprite = (i < starCount) ? filledStar : emptyStar;
                }
            }
        } else {
            Debug.LogError("StarGroup을 찾을 수 없습니다.");
        }
    }
}
