using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            gameObject.SetActive(false);
        }
    }
}
