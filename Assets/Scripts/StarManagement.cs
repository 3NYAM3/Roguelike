using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarManagement : Singleton<StarManagement> {

    //  protected override void Awake() {
    //      base.Awake();
    //  }
    
    private List<int> mapStars;

    private void Update() {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if(Input.GetKey(KeyCode.R)&& Input.GetKey(KeyCode.R)) {
                ResetAllStars();
            }
        }
    }

    public void InitializeMaps(int mapCount) {
        mapStars = new List<int>(new int[mapCount]);
        LoadStars();
    }

    public void SetStarsForMap(int mapIndex, int stars) {
        if (mapIndex >= 0 && mapIndex < mapStars.Count) {
            mapStars[mapIndex] = stars;
            SaveStars();
        } else {
            Debug.LogError("맵 인덱스가 잘못되었습니다.");
        }
    }

    public int GetStarsForMap(int mapIndex) {
        if (mapIndex >= 0 && mapIndex < mapStars.Count) {
            return mapStars[mapIndex];
        } else {
            Debug.LogError("맵 인덱스가 잘못되었습니다.");
            return 0;
        }
    }

    public List<int> GetAllMapStars() {
        return mapStars;
    }

    public void ResetAllStars() {
        for (int i = 0; i < mapStars.Count; i++) {
            mapStars[i] = 0;
        }
        SaveStars();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveStars() {
        for (int i = 0; i < mapStars.Count; i++) {
            PlayerPrefs.SetInt("MapStars" + i, mapStars[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadStars() {
        for (int i = 0; i < mapStars.Count; i++) {
            mapStars[i] = PlayerPrefs.GetInt("MapStars" + i, 0);
        }
    }
}
