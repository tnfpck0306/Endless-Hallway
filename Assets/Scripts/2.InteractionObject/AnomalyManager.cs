using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public GameObject[] exitLight; // 탈출구 지시등
    public GameObject[] doll; // 인형(0. 빨강/1. 파랑/)
    public GameObject[] hideDoll; // 숨은 인형
    public GameObject fireExting; // 소화기
    public GameObject palmTrigger; // 손바닥 이벤트 트리거

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
                HideDoll(0);
                fireExting.SetActive(false);
                break;

            case 3:
                HideDoll(1);
                break;

            case 4:
                HideDoll(2);
                break;

            case 5:
                HideDoll(3);
                break;

            case 6:
                HideDoll(4);
                break;

            case 7:
                HideDoll(5);
                break;
            
            case 8:
                palmTrigger.SetActive(true);
                break;
        }
    }

    private void HideDoll(int dollNum)
    {
        GameManager.instance.condition = false;
        doll[dollNum].SetActive(false);
        hideDoll[dollNum].SetActive(true);
    }

    public void FindeDoll(int anomalyNum)
    {
        GameManager.instance.condition = true;
        switch (anomalyNum)
        {
            case 2:
                doll[0].SetActive(true);
                hideDoll[0].SetActive(false);
                break;

            case 3:
                doll[1].SetActive(true);
                hideDoll[1].SetActive(false);
                break;

            case 4:
                doll[2].SetActive(true);
                hideDoll[2].SetActive(false);
                break;

            case 5:
                doll[3].SetActive(true);
                hideDoll[3].SetActive(false);
                break;

            case 6:
                doll[4].SetActive(true);
                hideDoll[4].SetActive(false);
                break;

            case 7:
                doll[5].SetActive(true);
                hideDoll[5].SetActive(false);
                break;

        }
    }
}
