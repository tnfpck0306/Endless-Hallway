using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public AudioSource[] audioSources; // ����Ŀ���� audiosource
    public float playDuration = 4.0f; // ����Ŀ ��� �ð�
    public float silenceDuration = 3.0f; // ������� �ʴ� �ð�
    public bool isPlayingSound = false; //�Ҹ� ��� ������ Ȯ�� ����

    public void SpeakerSound(AudioClip audioClip)
    {
        foreach (var source in audioSources)
        {
            source.clip = audioClip;
        }
        StartCoroutine(PlaySoundLoop());
    }

    // ����Ŀ�� �ݺ� ���
    IEnumerator PlaySoundLoop()
    {
        while (true)
        {
            foreach(var source in audioSources)
            {
                source.Play(); // ��� ����Ŀ���� ���
            }
            isPlayingSound = true;

            yield return new WaitForSeconds(playDuration);

            foreach(var source in audioSources)
            {
                source.Stop(); // ��� ����Ŀ���� ����
            }
            yield return new WaitForSeconds(0.6f); // �÷��̾��� ���� ����
            isPlayingSound = false;

            yield return new WaitForSeconds(silenceDuration);
        }
    }
}
