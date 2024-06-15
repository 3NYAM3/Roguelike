using UnityEngine;

public class StageClear : MonoBehaviour {
    [SerializeField] private GameObject coinManagerObject;
    [SerializeField] private float requiredHoldTime = 3f;

    private CoinManager coinManager;
    private bool isPlayerInRange = false;
    private float holdTime = 0f;
    

    void Start() {
        if (coinManagerObject != null) {
            coinManager = coinManagerObject.GetComponent<CoinManager>();
        } else {
            Debug.LogError("CoinManager 오브젝트가 할당되지 않았습니다.");
        }
    }

    void Update() {
        if (isPlayerInRange) {
            if (Input.GetKey(KeyCode.E)) {
                holdTime += Time.deltaTime;
                if (holdTime >= requiredHoldTime) {
                    StageComplete();
                    holdTime = 0f;
                }
            } else {
                holdTime = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            isPlayerInRange = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            isPlayerInRange = false;
            holdTime = 0f;
        }
    }

    void StageComplete() {
        if (coinManager != null) {
            int coinCount = coinManager.GetCoin();
            Debug.Log("획득한 코인 개수: " + coinCount);

        } else {
            Debug.LogError("CoinManagement 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
