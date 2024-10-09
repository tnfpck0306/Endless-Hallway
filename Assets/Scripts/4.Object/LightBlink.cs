using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    public GameObject spotLight;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 8)
        {
            StartCoroutine(blink());
            timer = 0;
        }
    }

    IEnumerator blink()
    {
        spotLight.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        spotLight.SetActive(false);

        yield return new WaitForSeconds(2f);

        spotLight.SetActive(true);
    }
}
