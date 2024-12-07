using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public float playDuration = 4.0f; // 스피커 재생 시간
    public float silenceDuration = 1.5f; // 재생하지 않는 시간
    public bool isPlayingSound = true; //소리 재생 중인지 확인 여부

    public GameObject fakeUI;
    public PlayerEventAudio evenetAudio;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SpeakerSound(AudioClip whiteNoiseClip, AudioClip clapEventClip)
    {
        audioSource.clip = whiteNoiseClip;
        StartCoroutine(PlaySoundLoop(clapEventClip));
    }

    // 스피커의 반복 재생
    IEnumerator PlaySoundLoop(AudioClip clapEventClip)
    {
        int count = 0;

        while (true)
        {
            // 스피커소리 재생
            audioSource.Play();
            isPlayingSound = true;
            yield return new WaitForSeconds(playDuration);

            // 스피커소리 정지
            audioSource.Stop();
            yield return new WaitForSeconds(1f); // 플레이어의 인지 간격
            isPlayingSound = false;

            if (count == 5)
            {
                fakeUI.SetActive(true);
            }

            yield return new WaitForSeconds(silenceDuration);

            if(count == 3)
            {
                evenetAudio.PlayerEventSound(clapEventClip);
            }
            yield return new WaitForSeconds(silenceDuration);

            if(count == 5)
            {
                fakeUI.transform.SetParent(null);
                fakeUI.GetComponent<Rigidbody>().useGravity = true;
            }

            count++;
        }
    }
}
