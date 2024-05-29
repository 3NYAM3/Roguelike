using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private PolygonCollider2D cameraConfiner;
    [SerializeField] private GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("����");

            var confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {

                confiner.m_BoundingShape2D = cameraConfiner;
                confiner.InvalidateCache();
                confiner.enabled = true;
            }

            door.SetActive(true);
            
            // Ʈ���� Collider ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
