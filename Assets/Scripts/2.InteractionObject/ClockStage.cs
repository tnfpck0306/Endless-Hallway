using System.Collections;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    public GameObject hourHand1; // �ð� �޸��� ��ħ
    public GameObject hourHand2; // �ð� �ո��� ��ħ

    void Start()
    {
        // ó�� �ð��� ȸ�� ����
        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);

    }

}
