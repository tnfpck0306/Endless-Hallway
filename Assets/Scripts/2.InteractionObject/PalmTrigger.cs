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
        int index = RedPalm.Length / 2;
        int count = index + 1;
        for (int i = 0; i < count; i++)
        {
            RedPalm[i].SetActive(true);
            
            if (i != 0)
            {
                RedPalm[i + index].SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }

            else
                yield return new WaitForSeconds(0.5f);
        }
    }
}
