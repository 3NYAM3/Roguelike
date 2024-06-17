using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour {
    private CinemachineConfiner2D confiner;
    private BoxCollider2D boxCollider;

    void Start() {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null) {
            confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null) {
                Debug.Log("CinemachineConfiner2D�� ã�ҽ��ϴ�.");
            } else {
                Debug.LogError("CinemachineConfiner2D ������Ʈ�� ã�� �� �����ϴ�!");
            }
        } else {
            Debug.LogError("CinemachineVirtualCamera�� ã�� �� �����ϴ�!");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PolygonCollider2D roomBounds = GetComponent<PolygonCollider2D>();
            if (roomBounds != null) {
                SetConfiner(roomBounds);
            } else {
                Debug.LogError("PolygonCollider2D�� ã�� �� �����ϴ�!");
            }
        }
    }

    void SetConfiner(PolygonCollider2D newBounds) {
        if (confiner != null && confiner.m_BoundingShape2D != newBounds) {
            confiner.m_BoundingShape2D = newBounds;
        } else if (confiner == null) {
            Debug.LogError("CinemachineConfiner2D�� null�Դϴ�.");
        }
    }
}
