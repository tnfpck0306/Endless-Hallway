using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 이상현상 동작 테스트를 위한 스크립트
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
