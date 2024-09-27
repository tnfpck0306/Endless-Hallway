using System.Collections;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    public GameObject hourHand1; // 시계 뒷면의 시침
    public GameObject hourHand2; // 시계 앞면의 시침

    void Start()
    {
        // 처음 시계의 회전 설정
        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);

    }

}
