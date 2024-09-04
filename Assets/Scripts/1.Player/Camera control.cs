using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Cameracontrol : MonoBehaviour
{
    public GameObject Flashlight;
    public ClickManager clickManager;
    public PlayerRotate playerRotate;

    [SerializeField]private float MouseSensitivity = 400f;
    private float MouseX;
    private float MouseY;

    private void Update()
    {
        if (!GameManager.instance.zoomIn) {

            if (clickManager.rayHitString == "Anomaly")
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }

            else if (clickManager.rayHitString == "Normal")
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            
            else
                Rotate();
        }

    }

    // ī�޶� �� ������ ���� ��ġ(Zoom-In ������ ��)
    public void Fixation(float x, float z)
    {
        playerRotate.playerState = PlayerRotate.rotateState.Stop;

        float boardY = clickManager.hit.transform.rotation.eulerAngles.y;
        boardY = (boardY + 180) % 360;
        transform.rotation = Quaternion.Euler(x, boardY, z);

        Flashlight.transform.rotation = transform.rotation;
    }

    // ���콺 ��ġ�� ���� ���� �� ������ �̵�
    private void Rotate()
    {
        MouseX += Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        MouseY -= Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        MouseX = Mathf.Clamp(MouseX, -90f, 90f);
        MouseY = Mathf.Clamp(MouseY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f);
        Flashlight.transform.localRotation = transform.localRotation;
    }
}
