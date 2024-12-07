using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public float playDuration = 4.0f; // ����Ŀ ��� �ð�
    public float silenceDuration = 1.5f; // ������� �ʴ� �ð�
    public bool isPlayingSound = true; //�Ҹ� ��� ������ Ȯ�� ����

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

    // ����Ŀ�� �ݺ� ���
    IEnumerator PlaySoundLoop(AudioClip clapEventClip)
    {
        int count = 0;

        while (true)
        {
            // ����Ŀ�Ҹ� ���
            audioSource.Play();
            isPlayingSound = true;
            yield return new WaitForSeconds(playDuration);

            // ����Ŀ�Ҹ� ����
            audioSource.Stop();
            yield return new WaitForSeconds(1f); // �÷��̾��� ���� ����
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
