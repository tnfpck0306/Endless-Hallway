using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera cameraA; // A 카메라
    public Camera cameraB; // B 카메라
    public GameObject menuPanel; // 메뉴 panel
    public GameObject returnButton; // 되돌아가기 버튼

    public void SwitchCamera()
    {
        if (cameraA != null && cameraB != null)
        {
            // A 카메라 비활성화, B 카메라 활성화
            cameraA.enabled = !cameraA.enabled;
            cameraB.enabled = !cameraB.enabled;
            menuPanel.SetActive(!menuPanel.activeSelf);
            returnButton.SetActive(!returnButton.activeSelf);
        }
    }
}
