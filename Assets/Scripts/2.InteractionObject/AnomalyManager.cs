using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public GameObject[] exitLight; // Ż�ⱸ ���õ�
    public GameObject[] doll; // ����(0. ����/1. �Ķ�/)
    public GameObject[] hideDoll; // ���� ����
    public GameObject fireExting; // ��ȭ��

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
        }
    }

    private void HideDoll(int dollNum)
    {
        doll[dollNum].SetActive(false);
        hideDoll[dollNum].SetActive(true);
    }

    public void FindeDoll(int anomalyNum)
    {
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
        }
    }
}
