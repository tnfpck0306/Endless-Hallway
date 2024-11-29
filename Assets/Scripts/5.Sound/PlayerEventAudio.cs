using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventAudio : MonoBehaviour
{
    private AudioSource audioSource; // ����� �̺�Ʈ �����

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerEventSound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
