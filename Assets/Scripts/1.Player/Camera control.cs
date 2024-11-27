using System.Collections;
using UnityEngine;
using static PlayerMovement;

public class Cameracontrol : MonoBehaviour
{
    public GameObject Flashlight;
    public ClickManager clickManager;
    public PlayerMovement playerMovement;
    public PlayerInven playerInven;

    public Camera playerCamera;
    public SpeakerControl control;

    [SerializeField]private float MouseSensitivity = 400f;
    private float xRotation = 0f;
    private Vector3 lastMousePosition;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (playerMovement.playerState != PlayerMovement.PlayerState.Limit) {
            Rotate();
        }

        NoMovement();

    }

    // 카메라 및 손전등 고정 위치(Zoom-In 상태일 때)
    public void Fixation(float x, float z)
    {
        playerCamera.fieldOfView = 30f; // 시야각 조정

        float boardY = clickManager.hit.transform.rotation.eulerAngles.y;
        boardY = (boardY + 180) % 360;
        transform.rotation = Quaternion.Euler(x, boardY, z);

        Flashlight.transform.rotation = transform.rotation;
    }

    // 마우스 위치에 따른 시점 및 손전등 이동
    private void Rotate()
    {
        // 마우스 입력
        float MouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // 카메라 상하 회전 제한
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라 상하 회전
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // 플레이어 몸체 좌우 회전
        transform.parent.transform.Rotate(Vector3.up * MouseX);
        
        // 카메라 회전에 맞춰 손전등 회전
        Flashlight.transform.localRotation = transform.localRotation;
    }

    // 움직임 금지
    private void NoMovement()
    {
        // 스피커 소리가 들리지 않을 때 움직임 금지(18번 이상현상)
        if (GameManager.instance.anomalyNum == 18)
        {
            if (!control.isPlayingSound && IsMouseMoved())
            {
                GameManager.instance.EndGame();
            }

            lastMousePosition = Input.mousePosition;
        }
    }

    // 마우스 움직임 감지
    private bool IsMouseMoved()
    {
        return lastMousePosition != Input.mousePosition;
    }
}
