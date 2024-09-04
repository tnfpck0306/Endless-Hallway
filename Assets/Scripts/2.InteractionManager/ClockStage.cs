using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    private ObjectRotate objectRotate;

    public GameObject hourHand1; // 시계 뒷면의 시침
    public GameObject hourHand2; // 시계 앞면의 시침
    public GameObject eventLocker;

    void Start()
    {
        objectRotate = GetComponent<ObjectRotate>();

        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);
    }

    void Update()
    {
        LockerMovement();
    }

    private void LockerMovement()
    {
        if (eventLocker.transform.localEulerAngles.y == 345f)
        {
            objectRotate.Rotation(-50f, eventLocker.transform);
        }
        
        if (eventLocker.transform.localEulerAngles.y == 295)
        {
            objectRotate.Rotation(50f, eventLocker.transform);
        }
    }
}
