using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public GameObject[] exitLight; // Ż�ⱸ ���õ�
    public GameObject[] doll; // ����(0. ����/1. �Ķ�/)
    public GameObject[] hideDoll; // ���� ����
    public GameObject fireExting; // ��ȭ��
    public GameObject palmTrigger; // �չٴ� �̺�Ʈ Ʈ����

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
