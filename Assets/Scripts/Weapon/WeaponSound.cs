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

    // �� �Ҹ� ��� �Լ�
    public void PlayGunSound() {
        audioSource.volume = 0.1f;
        PlaySound(gunSound);
    }

    // Ȱ �Ҹ� ��� �Լ�
    public void PlayBowSound() {
        audioSource.volume = 0.6f;
        PlaySound(bowSound);
    }

    // �� �Ҹ� ��� �Լ�
    public void PlaySwordSound() {
        audioSource.volume = 0.7f;
        PlaySound(swingSound);
    }

    // ���� �Ҹ� ��� �Լ�
    public void PlayMagicSound() {
        audioSource.volume = 0.3f;
        PlaySound(magicSound);
    }

    // �Ҹ� ����� ���� ���� �Լ�
    private void PlaySound(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }
}
