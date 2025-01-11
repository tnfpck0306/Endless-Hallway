using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestMode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI anomalyNumText;
    public bool testModeON;

    private void Start()
    {
        anomalyNumText.text = "TestMode\nAnomaly Number : "  + GameManager.instance.anomalyNum;
    }
}
