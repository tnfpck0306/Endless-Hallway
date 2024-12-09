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
    private AudioSource fakeUIAudio;

    public AudioClip[] anomalySFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fakeUIAudio = fakeUI.GetComponent<AudioSource>();
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

            if (count == 2)
            {
                fakeUI.SetActive(true);
                fakeUIAudio.clip = anomalySFX[0];
                fakeUIAudio.Play();
            }

            yield return new WaitForSeconds(silenceDuration);

            if(count == 4)
            {
                evenetAudio.PlayerEventSound(clapEventClip);
            }
            yield return new WaitForSeconds(silenceDuration);

            if(count == 2)
            {
                fakeUI.GetComponent<BoxCollider>().enabled = true;
                fakeUI.transform.SetParent(null);
                fakeUI.GetComponent<Rigidbody>().useGravity = true;
                yield return new WaitForSeconds(0.3f);
                fakeUI.transform.position = new Vector3(fakeUI.transform.position.x, 0.1f, fakeUI.transform.position.z);
                fakeUIAudio.clip = anomalySFX[1];
                fakeUIAudio.Play();
            }

            count++;
        }
    }
}
