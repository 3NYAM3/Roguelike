using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
    [SerializeField] private Sprite pressed, notPressed;

    private SpriteRenderer spriteRenderer;
    public bool IsPressed { get; private set; }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = notPressed;
        IsPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("MovableObject") || collision.gameObject.CompareTag("Player")) {
            spriteRenderer.sprite = pressed;
            IsPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("MovableObject") || collision.gameObject.CompareTag("Player")) {
            spriteRenderer.sprite = notPressed;
            IsPressed = false;
        }
    }
}
