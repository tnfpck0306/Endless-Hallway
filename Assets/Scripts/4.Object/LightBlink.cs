using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject footprint;
    public GameObject door;
    public GameObject eyes;

    private float timer = 0; // 손전등 타이머
    private int count; // 이상현상 효과 타이밍

    private void Start()
    {
        count = 0;
    }

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

        count++;
        footprintEffect();

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        eyes.SetActive(true);
        spotLight.SetActive(false);

        yield return new WaitForSeconds(2f);

        eyes.SetActive(false);
        spotLight.SetActive(true);
    }

    private void footprintEffect()
    {
        if (footprint.activeSelf)
        {
            door.SetActive(true);
            footprint.SetActive(false);
        }
        else
        {
            door.SetActive(false);
            footprint.SetActive(true);
        }
    }
}
