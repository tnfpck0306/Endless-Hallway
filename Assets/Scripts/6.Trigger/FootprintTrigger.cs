using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintTrigger : MonoBehaviour
{
    public GameObject[] footprint;
    public FootprintAudio footprintAudio;

    /// <summary>
    /// 10번 이상현상(피 묻은 발자국) 작동 트리거 스크립트
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Footprint());
            footprintAudio.StartFootprint();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator Footprint()
    {
        for (int i = 0; i < 10; i++)
        {
            footprint[i].SetActive(true);

            yield return new WaitForSeconds(0.12f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 10; i < footprint.Length; i++)
        {
            footprint[i].SetActive(true);

            yield return new WaitForSeconds(0.12f);
        }
    }
}
