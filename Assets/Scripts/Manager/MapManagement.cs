using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManagement : MonoBehaviour {
    [SerializeField] private List<GameObject> walls;

    private PolygonCollider2D room;
    private bool isWallActive = false;
    private GameObject[] enemies;
    private bool enemyInRoom = false;

    private void Awake() {
        room = gameObject.GetComponent<PolygonCollider2D>();
        foreach (GameObject wall in walls) {
            wall.SetActive(false);
        }
    }
    private void Update() {
        CheckPlayerInRoom();
        enemyInRoom = CheckEnemiesInRoom();
        if (!enemyInRoom) {
            ChangeWallActive();
        }
    }

    private void CheckPlayerInRoom() {
        if (EnterRoom()) {
            ChangeWallActive();
        }
    }

    private bool CheckEnemiesInRoom() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) {
            if (room.OverlapPoint(enemy.transform.position)) {
                return true;
            }
        }
        return false;
    }

    private bool EnterRoom() {
        return room.OverlapPoint(PlayerController.Instance.transform.position);
    }

    private void ChangeWallActive() {
        if (!EnterRoom()) {
            isWallActive = false;
        } else if (!isWallActive) {
            isWallActive = true;
            foreach (var wall in walls) {
                wall.SetActive(true);
            }
        } else if (isWallActive && !enemyInRoom) {
            isWallActive = false;
            foreach (var wall in walls) {
                wall.SetActive(false);
            }
        }
    }


}

