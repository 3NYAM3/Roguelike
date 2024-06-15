using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIManager : MonoBehaviour {
    [SerializeField] private List<GameObject> map;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite filledStar;

    void Start() {
        UpdateAllStars();
    }

    public void UpdateAllStars() {
        for (int i = 0; i < map.Count; i++) {
            UpdateStars(i);
        }
    }

    public void UpdateStars(int mapIndex) {
        int starCount = StarManagement.Instance.GetStarsForMap(mapIndex);
        Transform starGroup = map[mapIndex].transform.Find("Stars");

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
