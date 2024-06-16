using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUpSound : MonoBehaviour
{
    [SerializeField] private AudioClip coin;

    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void coinSound() {
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(coin);
    }
}
