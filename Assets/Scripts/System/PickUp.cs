using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private enum PickUpType {
        Coin,
        HPOrb,
        Cube
    }
    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir;
    private Rigidbody2D rb;
    private bool canMove = false;
    private PlayerHealth playerHealth;
    private CoinManager coinManager;

 

    const string COIN_AMOUNT_TEXT = "COIN AMOUNT TEXT";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = PlayerController.Instance.GetComponent<PlayerHealth>();
        coinManager = FindAnyObjectByType<CoinManager>();
    }

    private void Start()
    {
        if (gameObject.CompareTag("AutoPickUp"))
        {
            StartCoroutine(AnimCurveSpawnRoutine());
        }
            
    }

    private void Update()
    {
        if (gameObject.CompareTag("AutoPickUp"))
        {
            Vector3 playerPos = PlayerController.Instance.transform.position;

            if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
            {
                moveDir = (playerPos - transform.position).normalized;
                moveSpeed += accelartionRate;
            }
            else
            {
                moveDir = Vector3.zero;
                moveSpeed = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            rb.velocity = moveDir * moveSpeed * Time.deltaTime;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 2f);
        
        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while(timePassed<popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }

        canMove = true;
    }

    private void DetectPickupType() {
        switch(pickUpType) {
            case PickUpType.Coin:
                coinManager.UPdateCurrentCoin();
                Debug.Log("Coin");
                break;
            case PickUpType.HPOrb:
                playerHealth.HpOrbGet();
                Debug.Log(" HPorb");
                break;
            case PickUpType.Cube:
                Debug.Log(" Cube");
                break;
        }
    }
}
