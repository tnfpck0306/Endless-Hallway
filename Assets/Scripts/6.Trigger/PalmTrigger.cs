using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 8번 이상현상(피 묻은 손바닥) 작동 트리거 스크립트
/// </summary>
public class PalmTrigger : MonoBehaviour
{
    public GameObject[] RedPalm; // 피 묻은 손바닥
    public AudioSource audioSource; // 손바닥 AudioSource
    public AudioManager audioManager; // 손바닥 사운드 클립
    public Light flashLight; // 손전등

    private bool EndCheck = false; // 트리거 종료 여부

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Handprint());
            StartCoroutine(blink());
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator Handprint()
    {
        int printMiddle = RedPalm.Length / 2;
        int count = printMiddle + 1;

        int printQuarter = (count / 2) + 1;
        float interval = 0.35f;

        for (int i = 0; i < printQuarter; i++)
        {

            RedPalm[i].SetActive(true);
            audioSource.clip = audioManager.preloadClips[5];
            audioSource.Play();

            if (i != 0)
            {
                RedPalm[count - i].SetActive(true);
                RedPalm[i + printMiddle].SetActive(true);
                RedPalm[RedPalm.Length - i].SetActive(true);
                audioSource.clip = audioManager.preloadClips[6];
                audioSource.Play();
                yield return new WaitForSeconds(interval);
            }

            else
                yield return new WaitForSeconds(0.7f);

            if (interval > 0.15f)
                interval -= 0.05f;
        }

        EndCheck = true;
    }

    IEnumerator blink()
    {
        yield return new WaitForSeconds(0.7f);

        while (!EndCheck) {
            flashLight.enabled = false;

            yield return new WaitForSeconds(0.1f);

            flashLight.enabled = true;

            yield return new WaitForSeconds(0.1f);
        }

    }
}
