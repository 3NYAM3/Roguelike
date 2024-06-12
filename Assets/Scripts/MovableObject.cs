using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isMoving = false; // �߰��� �κ�

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() // ������ �޼ҵ�
    {
        if (isMoving) {
            Move();
        }
    }

    public void StartMoving() // �߰��� �޼ҵ�
    {
        isMoving = true;
    }

    public void StopMoving() // �߰��� �޼ҵ�
    {
        isMoving = false;
    }

    public void Move() // ������ �޼ҵ�
    {
        Vector2 direction = DetermineDirection();
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    private Vector2 DetermineDirection() // �߰��� �޼ҵ�
    {
        Vector2 playerPosition = PlayerController.Instance.transform.position;
        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(transform.position.x - playerPosition.x) > Mathf.Abs(transform.position.y - playerPosition.y)) {
            // Horizontal movement
            direction.x = transform.position.x > playerPosition.x ? 1 : -1;
        } else {
            // Vertical movement
            direction.y = transform.position.y > playerPosition.y ? 1 : -1;
        }

        return direction;
    }
}