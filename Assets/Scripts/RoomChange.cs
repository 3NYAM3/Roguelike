using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{
    private CinemachineConfiner2D confiner; // CinemachineConfiner2D�� ���
    private BoxCollider2D boxCollider;

    void Start()
    {
        // Cinemachine Virtual Camera���� CinemachineConfiner2D ������Ʈ�� ã��
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            Debug.Log("CinemachineVirtualCamera�� ã�ҽ��ϴ�.");
            confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                Debug.Log("CinemachineConfiner2D�� ã�ҽ��ϴ�.");
            }
            else
            {
                Debug.LogError("CinemachineConfiner2D ������Ʈ�� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera�� ã�� �� �����ϴ�!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D ȣ���.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�� Ʈ���ſ� �����߽��ϴ�.");
            // Ʈ���Ÿ� ���� ���� ������Ʈ�� PolygonCollider2D�� ��������
            PolygonCollider2D roomBounds = GetComponent<PolygonCollider2D>();
            if (roomBounds != null)
            {
                Debug.Log("PolygonCollider2D�� ã�ҽ��ϴ�.");
                // CinemachineConfiner2D�� BoundingShape2D�� ���� PolygonCollider2D�� ����
                SetConfiner(roomBounds);
            }
            else
            {
                Debug.LogError("PolygonCollider2D�� ã�� �� �����ϴ�!");
            }
        }
    }

    void SetConfiner(PolygonCollider2D newBounds)
    {
        Debug.Log("SetConfiner ȣ���.");
        if (confiner != null && confiner.m_BoundingShape2D != newBounds)
        {
            Debug.Log("BoundingShape2D�� �� ���� �����մϴ�.");
            confiner.m_BoundingShape2D = newBounds;
        }
        else if (confiner == null)
        {
            Debug.LogError("CinemachineConfiner2D�� null�Դϴ�.");
        }
    }
}
