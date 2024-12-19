using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̻����� ������ ���� ��ũ��Ʈ(�߰��� �̻�������� Ȯ��)
/// </summary>
public class CheckAnomaly : MonoBehaviour
{
    [SerializeField] private GameObject[] check;
    [SerializeField] private GameObject[] interactionColider;
    [SerializeField] private GameObject[] anomalyPicture; // �̻����� ����
    [SerializeField] private GameObject anomalyPicturePanel; // �̻����� ���� �г�
    [SerializeField] private int countAnomaly; // �̻����� ����

    private bool pictureActive; // ���� �г� ����
    private string clickNum; // Ŭ���� �̻����� ��ȣ

    void Start()
    {
        pictureActive = false;

        for(int i = 1; i < check.Length; i++)
        {
            string anomalyKey = i.ToString();
            if(PlayerPrefs.GetInt(anomalyKey) == 1)
            {
                check[i].SetActive(true);
                interactionColider[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pictureActive)
        {
            DetectObjectUnderMouse();
        }
        else if (Input.GetMouseButtonDown(0) && pictureActive)
        {
            anomalyPicture[int.Parse(clickNum)].SetActive(false);
            anomalyPicturePanel.SetActive(false);
            pictureActive = false;
        }
    }

    void DetectObjectUnderMouse()
    {
        // ī�޶󿡼� ���콺 ��ġ�� �������� Ray ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast�� ���� �浹�� ������Ʈ ����
        if (Physics.Raycast(ray, out hit))
        {
            clickNum = hit.collider.gameObject.name;

            for(int i = 1; i <= countAnomaly; i++)
            {
                if (clickNum == i.ToString())
                {
                    anomalyPicturePanel.SetActive(true);
                    anomalyPicture[i].SetActive(true);
                    pictureActive = true;
                }
            }

                
        }

    }
}
