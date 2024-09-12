using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 마우스 클릭을 통해 오브젝트 테그를 인식하고 다른 스크립트들에게 알리는 역할
/// </summary>
public class ClickManager : MonoBehaviour
{
    public InteractionManager interactionManager;

    public Ray ray;
    public RaycastHit hit;
    public GameObject interactionObj;
    public string rayHitString;

    private void Update()
    {
        // 마우스 왼쪽 클릭
        if (Input.GetMouseButtonDown(0))
        {
            RayClick();
            interactionManager.Interaction(interactionObj);
        }
    }

    // UI Zoom-out(되돌아가기) 버튼
    public void ZoomOut()
    {
        rayHitString = "ZoomOut";
        interactionManager.Interaction(interactionObj);
    }

    private void RayClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            interactionObj = hit.transform.gameObject;
            rayHitString = interactionObj.tag;
            Debug.Log(rayHitString);
        }

    }
   
}
