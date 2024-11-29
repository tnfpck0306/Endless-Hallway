using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventAudio : MonoBehaviour
{
    private AudioSource audioSource; // 사용자 이벤트 오디오

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
