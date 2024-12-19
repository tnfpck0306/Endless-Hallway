using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이상현상 도감에 대한 스크립트(발견한 이상현상들을 확인)
/// </summary>
public class CheckAnomaly : MonoBehaviour
{
    [SerializeField] private GameObject[] check;
    [SerializeField] private GameObject[] interactionColider;
    [SerializeField] private GameObject[] anomalyPicture; // 이상현상 사진
    [SerializeField] private GameObject anomalyPicturePanel; // 이상현상 사진 패널
    [SerializeField] private int countAnomaly; // 이상현상 개수

    private bool pictureActive; // 사진 패널 여부
    private string clickNum; // 클릭한 이상현상 번호

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
        // 카메라에서 마우스 위치를 기준으로 Ray 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast를 통해 충돌한 오브젝트 감지
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
