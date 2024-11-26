using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerControl : MonoBehaviour
{
    public float playDuration = 4.0f; // ����Ŀ ��� �ð�
    public float silenceDuration = 3.0f; // ������� �ʴ� �ð�
    public bool isPlayingSound = false; //�Ҹ� ��� ������ Ȯ�� ����

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

    // ����Ŀ�� �ݺ� ���
    IEnumerator PlaySoundLoop()
    {
        while (true)
        {
            audioSource.Play(); // ��� ����Ŀ���� ���
            isPlayingSound = true;

            yield return new WaitForSeconds(playDuration);

            audioSource.Stop(); // ��� ����Ŀ���� ����
            yield return new WaitForSeconds(0.6f); // �÷��̾��� ���� ����
            isPlayingSound = false;

            yield return new WaitForSeconds(silenceDuration);
        }
    }
}
