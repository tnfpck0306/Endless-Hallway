using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    public GameObject hourHand1;
    public GameObject hourHand2;

    void Start()
    {
        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
