using System.Collections;
using UnityEngine;

public class Cameracontrol : MonoBehaviour
{
    public GameObject Flashlight;
    public ClickManager clickManager;
    public PlayerMovement playerMovement;
    public PlayerInven playerInven;

    public Camera playerCamera;

    [SerializeField]private float MouseSensitivity = 400f;
    private float xRotation = 0f;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (playerMovement.playerState != PlayerMovement.PlayerState.Limit) {
            Rotate();
        }

    }

    // ī�޶� �� ������ ���� ��ġ(Zoom-In ������ ��)
    public void Fixation(float x, float z)
    {
        playerCamera.fieldOfView = 30f; // �þ߰� ����

        float boardY = clickManager.hit.transform.rotation.eulerAngles.y;
        boardY = (boardY + 180) % 360;
        transform.rotation = Quaternion.Euler(x, boardY, z);

        Flashlight.transform.rotation = transform.rotation;
    }

    // ���콺 ��ġ�� ���� ���� �� ������ �̵�
    private void Rotate()
    {
        // ���콺 �Է�
        float MouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // ī�޶� ���� ȸ�� ����
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶� ���� ȸ��
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // �÷��̾� ��ü �¿� ȸ��
        transform.parent.transform.Rotate(Vector3.up * MouseX);
        
        // ī�޶� ȸ���� ���� ������ ȸ��
        Flashlight.transform.localRotation = transform.localRotation;
    }
}
