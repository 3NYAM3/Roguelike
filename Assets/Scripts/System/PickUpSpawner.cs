using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject HpUp;

    private Vector2 hpUpOffset;
    private Vector3 hpUpPosition;
    private int random;

    public void DropItems()
    {
        Instantiate(coin, transform.position, Quaternion.identity);

        random = Random.Range(0, 10);
        if(random == 0)
        {
            hpUpOffset = Random.insideUnitCircle;
            hpUpPosition = transform.position + new Vector3(hpUpOffset.x, hpUpOffset.y);
            Instantiate(HpUp, hpUpPosition, Quaternion.identity);
        }

        
    }
}
