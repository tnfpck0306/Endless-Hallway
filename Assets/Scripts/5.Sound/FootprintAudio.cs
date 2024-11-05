using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    public float duration = 0.1f;
    public float speed = 200f;
    private float elapsedTime = 0f;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartFootprint()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        StartCoroutine(Move());
    }

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

        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.Play();

        yield return null;
    }
}
