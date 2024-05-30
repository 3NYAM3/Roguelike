using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{
    private CinemachineConfiner2D confiner; // CinemachineConfiner2D를 사용
    private BoxCollider2D boxCollider;

    void Start()
    {
        // Cinemachine Virtual Camera에서 CinemachineConfiner2D 컴포넌트를 찾기
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            Debug.Log("CinemachineVirtualCamera를 찾았습니다.");
            confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                Debug.Log("CinemachineConfiner2D를 찾았습니다.");
            }
            else
            {
                Debug.LogError("CinemachineConfiner2D 컴포넌트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera를 찾을 수 없습니다!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D 호출됨.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player가 트리거에 진입했습니다.");
            // 트리거를 가진 게임 오브젝트의 PolygonCollider2D를 가져오기
            PolygonCollider2D roomBounds = GetComponent<PolygonCollider2D>();
            if (roomBounds != null)
            {
                Debug.Log("PolygonCollider2D를 찾았습니다.");
                // CinemachineConfiner2D의 BoundingShape2D를 방의 PolygonCollider2D로 설정
                SetConfiner(roomBounds);
            }
            else
            {
                Debug.LogError("PolygonCollider2D를 찾을 수 없습니다!");
            }
        }
    }

    void SetConfiner(PolygonCollider2D newBounds)
    {
        Debug.Log("SetConfiner 호출됨.");
        if (confiner != null && confiner.m_BoundingShape2D != newBounds)
        {
            Debug.Log("BoundingShape2D를 새 경계로 설정합니다.");
            confiner.m_BoundingShape2D = newBounds;
        }
        else if (confiner == null)
        {
            Debug.LogError("CinemachineConfiner2D가 null입니다.");
        }
    }
}
