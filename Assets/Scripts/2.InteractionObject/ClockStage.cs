using System.Collections;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    public GameObject hourHand1; // �ð� �޸��� ��ħ
    public GameObject hourHand2; // �ð� �ո��� ��ħ
    
    public Transform eventLocker; // ��Ŀ���� Transform
    private Vector3 openRotation = new Vector3(0f, -80f, 0f); // ���� ���� �� ȸ�� ����
    private Vector3 closeRotation = new Vector3(0f, -15f, 0f); // ���� ���� �� ȸ�� ����
    private float speed = 0.8f; // ���� ���� �ݴ� �ӵ�
    private float openTime = 5.0f; // ���� ���� �ִ� �ð�
    private float closedTime = 5.0f; // ���� ���� �ִ� �ð�
    
    private bool isOpening = false; // ���� �������� ����
    private bool isClosing = false; // ���� �������� ����
    private float timer = 0.0f; // Ÿ�̸Ӹ� ���� ����

    void Start()
    {
        // ó�� �ð��� ȸ�� ����
        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);

        // ó�� ��Ŀ ���� ���� ���·� ����
        eventLocker.localRotation = Quaternion.Euler(closeRotation);
        isClosing = true;

    }

    void Update()
    {
        timer += Time.deltaTime;

        // �� ������ ��
        if (isClosing)
        {
            eventLocker.localRotation = Quaternion.Lerp(eventLocker.localRotation, Quaternion.Euler(closeRotation), Time.deltaTime * speed);

            if(timer  > closedTime)
            {
                // �ð��� ������ �� ����
                isClosing = false;
                isOpening = true;
                timer = 0.0f;
            }
        }
        // �� ������ ��
        else if(isOpening)
        {
            eventLocker.localRotation = Quaternion.Lerp(eventLocker.localRotation, Quaternion.Euler(openRotation), Time.deltaTime * speed);

            if (timer > openTime)
            {
                // �ð��� ������ �� �ݱ�
                isOpening = false;
                isClosing = true;
                timer = 0.0f;
            }
        }
    }

}
