using System.Collections;
using UnityEngine;

public class ClockStage : MonoBehaviour
{
    public GameObject hourHand1; // 시계 뒷면의 시침
    public GameObject hourHand2; // 시계 앞면의 시침
    
    public Transform eventLocker; // 라커문의 Transform
    private Vector3 openRotation = new Vector3(0f, -80f, 0f); // 문이 열릴 때 회전 각도
    private Vector3 closeRotation = new Vector3(0f, -15f, 0f); // 문이 닫힐 때 회전 각도
    private float speed = 0.8f; // 문의 열고 닫는 속도
    private float openTime = 5.0f; // 문이 열려 있는 시간
    private float closedTime = 5.0f; // 문이 닫혀 있는 시간
    
    private bool isOpening = false; // 문이 열리는지 여부
    private bool isClosing = false; // 문이 닫히는지 여부
    private float timer = 0.0f; // 타이머를 위한 변수

    void Start()
    {
        // 처음 시계의 회전 설정
        hourHand1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * 30f);
        hourHand2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GameManager.instance.stage * -30f);

        // 처음 라커 문을 닫힌 상태로 설정
        eventLocker.localRotation = Quaternion.Euler(closeRotation);
        isClosing = true;

    }

    void Update()
    {
        timer += Time.deltaTime;

        // 문 닫히는 중
        if (isClosing)
        {
            eventLocker.localRotation = Quaternion.Lerp(eventLocker.localRotation, Quaternion.Euler(closeRotation), Time.deltaTime * speed);

            if(timer  > closedTime)
            {
                // 시간이 지나면 문 열기
                isClosing = false;
                isOpening = true;
                timer = 0.0f;
            }
        }
        // 문 열리는 중
        else if(isOpening)
        {
            eventLocker.localRotation = Quaternion.Lerp(eventLocker.localRotation, Quaternion.Euler(openRotation), Time.deltaTime * speed);

            if (timer > openTime)
            {
                // 시간이 지나면 문 닫기
                isOpening = false;
                isClosing = true;
                timer = 0.0f;
            }
        }
    }

}
