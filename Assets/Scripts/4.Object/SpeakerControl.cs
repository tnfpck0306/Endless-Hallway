using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public float playDuration = 4.0f; // 스피커 재생 시간
    public float silenceDuration = 3.0f; // 재생하지 않는 시간
    public bool isPlayingSound = false; //소리 재생 중인지 확인 여부

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SpeakerSound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        StartCoroutine(PlaySoundLoop());
    }

    // 스피커의 반복 재생
    IEnumerator PlaySoundLoop()
    {
        while (true)
        {
            audioSource.Play(); // 모든 스피커에서 재생
            isPlayingSound = true;

            yield return new WaitForSeconds(playDuration);

            audioSource.Stop(); // 모든 스피커에서 정지
            yield return new WaitForSeconds(0.6f); // 플레이어의 인지 간격
            isPlayingSound = false;

            yield return new WaitForSeconds(silenceDuration);
        }
    }
}
