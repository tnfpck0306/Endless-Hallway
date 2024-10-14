using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintTrigger : MonoBehaviour
{
    public GameObject[] footprint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Footprint());
        }
    }

    IEnumerator Footprint()
    {
        for (int i = 0; i < 10; i++)
        {
            footprint[i].SetActive(true);

            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 10; i < footprint.Length; i++)
        {
            footprint[i].SetActive(true);

            yield return new WaitForSeconds(0.15f);
        }
    }
}
