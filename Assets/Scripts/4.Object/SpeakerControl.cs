using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public AudioSource[] audioSources; // 스피커들의 audiosource
    public float playDuration = 4.0f; // 스피커 재생 시간
    public float silenceDuration = 3.0f; // 재생하지 않는 시간
    public bool isPlayingSound = false; //소리 재생 중인지 확인 여부

    public void SpeakerSound(AudioClip audioClip)
    {
        foreach (var source in audioSources)
        {
            source.clip = audioClip;
        }
        StartCoroutine(PlaySoundLoop());
    }

    // 스피커의 반복 재생
    IEnumerator PlaySoundLoop()
    {
        while (true)
        {
            foreach(var source in audioSources)
            {
                source.Play(); // 모든 스피커에서 재생
            }
            isPlayingSound = true;

            yield return new WaitForSeconds(playDuration);

            foreach(var source in audioSources)
            {
                source.Stop(); // 모든 스피커에서 정지
            }
            yield return new WaitForSeconds(0.6f); // 플레이어의 인지 간격
            isPlayingSound = false;

            yield return new WaitForSeconds(silenceDuration);
        }
    }
}
