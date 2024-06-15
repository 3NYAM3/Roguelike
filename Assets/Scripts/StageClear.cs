using UnityEngine;
using UnityEngine.UI;

public class StageClear : MonoBehaviour {
    [SerializeField] private GameObject coinManagerObject;
    [SerializeField] private float requiredHoldTime = 3f;
    [SerializeField] private int mapIndex;
    [SerializeField] private GameObject result;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;

    private CoinManager coinManager;
    private PlayerHealth health;
    private bool isPlayerInRange = false;
    private float holdTime = 0f;
    public int coinCount { get; private set; }
    public float currentHpPercent { get; private set; }
    

    void Start() {
        if (coinManagerObject != null) {
            coinManager = coinManagerObject.GetComponent<CoinManager>();
        } else {
            Debug.LogError("CoinManager 오브젝트가 할당되지 않았습니다.");
        }

        health = PlayerController.Instance.GetComponent<PlayerHealth>();
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
            coinCount = coinManager.GetCoin();
            currentHpPercent = health.getHpPercent();
            Debug.Log("획득한 코인 개수: " + coinCount);
            Debug.Log("HP% : "+ currentHpPercent);

            int star = 1;
            if(coinCount == 30) {
                star++;
            }
            if (currentHpPercent >= 0.5f) {
                star++;
            }

            Transform starGroup = result.transform.Find("Stars");

            if (starGroup != null) {
                for (int i = 0; i < star; i++) {
                    Image starImage = starGroup.GetChild(i).GetComponent<Image>();
                    if (starImage != null) {
                        starImage.sprite = (i < star) ? filledStar : emptyStar;
                    }
                }
            } else {
                Debug.LogError("StarGroup을 찾을 수 없습니다.");
            }

            result.SetActive(true);

            StarManagement.Instance.SetStarsForMap(mapIndex, star);

        } else {
            Debug.LogError("CoinManagement 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
