using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private AudioClip swingSound;
    [SerializeField] private AudioClip gunSound;
    [SerializeField] private AudioClip bowSound;
    [SerializeField] private AudioClip magicSound;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        
    }

    // 총 소리 재생 함수
    public void PlayGunSound() {
        audioSource.volume = 0.1f;
        PlaySound(gunSound);
    }

    // 활 소리 재생 함수
    public void PlayBowSound() {
        audioSource.volume = 0.6f;
        PlaySound(bowSound);
    }

    // 검 소리 재생 함수
    public void PlaySwordSound() {
        audioSource.volume = 0.7f;
        PlaySound(swingSound);
    }

    // 마법 소리 재생 함수
    public void PlayMagicSound() {
        audioSource.volume = 0.3f;
        PlaySound(magicSound);
    }

    // 소리 재생을 위한 공통 함수
    private void PlaySound(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }
}
