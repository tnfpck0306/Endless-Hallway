using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �̻����� ���� �׽�Ʈ�� ���� ��ũ��Ʈ
/// </summary>
public class TestMode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI anomalyNumText;
    public bool testModeON;

    private void Start()
    {
        if(testModeON)
            anomalyNumText.text = "TestMode\nAnomaly Number : "  + GameManager.instance.anomalyNum;
    }
}
