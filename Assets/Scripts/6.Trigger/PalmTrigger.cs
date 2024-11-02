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
        int printMiddle = RedPalm.Length / 2;
        int count = printMiddle + 1;

        int printQuarter = (count / 2) + 1;
        float interval = 0.35f;
        for (int i = 0; i < printQuarter; i++)
        {

            RedPalm[i].SetActive(true);

            if (i != 0)
            {
                RedPalm[count - i].SetActive(true);
                RedPalm[i + printMiddle].SetActive(true);
                RedPalm[RedPalm.Length - i].SetActive(true);
                yield return new WaitForSeconds(interval);
            }

            else
                yield return new WaitForSeconds(0.7f);

            if (interval > 0.1f)
                interval -= 0.05f;
        }
    }
}
