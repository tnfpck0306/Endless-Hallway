using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTrigger : MonoBehaviour
{
    public GameObject[] RedPalm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Handprint());
        }
    }

    IEnumerator Handprint()
    {
        for (int i = 0; i < RedPalm.Length; i++)
        {
            RedPalm[i].SetActive(true);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
