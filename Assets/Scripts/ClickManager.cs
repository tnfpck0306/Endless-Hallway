using System.Collections;
using UnityEngine;

/// <summary>
/// 마우스 클릭을 통해 오브젝트 테그를 인식하고 다른 스크립트들에게 알리는 역할
/// </summary>
public class ClickManager : MonoBehaviour
{
    public InteractionManager interactionManager;
    public GameObject player;
    public GameObject InteratctionReticle;

    public Ray ray;
    public RaycastHit hit;
    public GameObject interactionObj; // 상호작용 하는 오브젝트
    public string rayHitString; // 상호작용 하는 오브젝트의 테그'string'

    private void Start()
    {
        // 커서를 화면 중간에 고정, 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RayPoint();
        float distance = Vector3.Distance(player.transform.position, interactionObj.transform.position);
        
        // 상호작용 가능한 오브젝트가 일정 거리 안에 있을 때
        if (rayHitString != "Untagged" && distance < 4.0f)
        {
            InteratctionReticle.SetActive(true); // 상호작용 조준점으로 ui 활성화

            // 마우스 좌클릭
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(rayHitString);
                interactionManager.Interaction(interactionObj);
            }
        }
        else
        {
            InteratctionReticle.SetActive(false); // 상호작용 조준점으로 ui 비활성화
        }

        // 마우스 우클릭
        if (Input.GetMouseButtonDown(1))
        {
            rayHitString = "ZoomOut";
            interactionManager.Interaction(interactionObj);
        }
    }

    // 커서 위치에 존재하는 오브젝트 확인
    private void RayPoint()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            interactionObj = hit.transform.gameObject;
            rayHitString = interactionObj.tag;
        }

    }
   
}
