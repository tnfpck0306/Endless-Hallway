using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 손전등 깜빡임 및 메인메뉴 이벤트를 위한 스크립트
/// </summary>
public class LightBlink : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject footprint;
    public GameObject door;
    public GameObject eyes;
    public GameObject monster;

    private bool isWork = false; // 작동 여부
    private float timer = 0; // 손전등 타이머
    private int count; // 이상현상 효과 타이밍

    private void Start()
    {
        count = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3 && !isWork)
        {
            isWork = true;
            StartCoroutine(blink());
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

        timer = 0;
        isWork = false;
    }

    private void footprintEffect()
    {
        if (footprint.activeSelf)
        {
            door.SetActive(true);
            monster.SetActive(false);
            footprint.SetActive(false);
        }
        else
        {
            door.SetActive(false);
            monster.SetActive(true);
            footprint.SetActive(true);
        }
    }
}
