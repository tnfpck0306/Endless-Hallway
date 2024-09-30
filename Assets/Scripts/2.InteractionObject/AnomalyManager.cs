using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public GameObject[] exitLight; // 탈출구 지시등
    public GameObject[] doll; // 인형
    public GameObject[] hideDoll; // 숨은 인형
    public GameObject fireExting; // 소화기

    private void Start()
    {
        placeAnomaly(GameManager.instance.anomalyNum);
    }

    public void placeAnomaly(int anomalyNum)
    {
        switch(anomalyNum)
        {
            case 0:
                break;

            case 1:
                exitLight[0].SetActive(false);
                exitLight[1].SetActive(true);
                break;

            case 2:
                doll[0].SetActive(false);
                hideDoll[0].SetActive(true);
                fireExting.SetActive(false);
                break;
        }
    }

    public void FindeDoll(int anomalyNum)
    {
        switch (anomalyNum)
        {
            case 2:
                doll[0].SetActive(true);
                hideDoll[0].SetActive(false);
                break;
        }
    }
}
