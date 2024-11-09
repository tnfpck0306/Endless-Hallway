using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 10번 이상현상(발자국) 사운드 스크립트
/// </summary>
public class FootprintAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public float duration = 0.1f;
    public float speed = 200f;
    private float elapsedTime = 0f;

    private void Start()
    {
        // 발자국 오디오 소스
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartFootprint()
    {
        audioSource.clip = audioClips[0]; // 발자국 소리 클립
        audioSource.Play();
        StartCoroutine(Move());
    }

    // 발자국을 따라 오디오 이동
    IEnumerator Move()
    {
        while(elapsedTime < duration)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(0.25f);
        }

        elapsedTime = 0f;
        duration = 0.03f;

        while (elapsedTime < duration)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime) ;
            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(0.25f);
        }

        // 천장 부딫치는 소리로 클립 변환
        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.Play();

        yield return null;
    }
}
