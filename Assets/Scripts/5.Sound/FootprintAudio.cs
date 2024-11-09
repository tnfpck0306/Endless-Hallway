using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 10�� �̻�����(���ڱ�) ���� ��ũ��Ʈ
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
        // ���ڱ� ����� �ҽ�
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartFootprint()
    {
        audioSource.clip = audioClips[0]; // ���ڱ� �Ҹ� Ŭ��
        audioSource.Play();
        StartCoroutine(Move());
    }

    // ���ڱ��� ���� ����� �̵�
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

        // õ�� �΋Hġ�� �Ҹ��� Ŭ�� ��ȯ
        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.Play();

        yield return null;
    }
}
