using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera cameraA; // A ī�޶�
    public Camera cameraB; // B ī�޶�
    public GameObject menuPanel; // �޴� panel
    public GameObject returnButton; // �ǵ��ư��� ��ư

    [SerializeField] private CheckAnomaly checkAnomaly;

    public void SwitchCamera()
    {
        if (cameraA != null && cameraB != null)
        {
            // A ī�޶� ��Ȱ��ȭ, B ī�޶� Ȱ��ȭ
            cameraA.enabled = !cameraA.enabled;
            cameraB.enabled = !cameraB.enabled;
            menuPanel.SetActive(!menuPanel.activeSelf);
            returnButton.SetActive(!returnButton.activeSelf);
            checkAnomaly.enabled = !checkAnomaly.enabled;
        }
    }
}
