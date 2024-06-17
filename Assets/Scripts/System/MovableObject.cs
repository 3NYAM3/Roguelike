using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private bool isMoving = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isMoving) {
            Move();
        }
    }

    public void StartMoving() {
        isMoving = true;
    }

    public void StopMoving() {
        isMoving = false;
    }

    public void Move() {
        Vector2 direction = DetermineDirection();
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    private Vector2 DetermineDirection() {
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