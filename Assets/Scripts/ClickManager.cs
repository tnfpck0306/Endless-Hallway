using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���콺 Ŭ���� ���� ������Ʈ �ױ׸� �ν��ϰ� �ٸ� ��ũ��Ʈ�鿡�� �˸��� ����
/// </summary>
public class ClickManager : MonoBehaviour
{
    public InteractionManager interactionManager;

    public Ray ray;
    public RaycastHit hit;
    public GameObject interactionObj;
    public string rayHitString;

    private void Start()
    {
        // ���콺�� ȭ�� �߰��� ����, Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // ���콺 ���� Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            RayClick();
            interactionManager.Interaction(interactionObj);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rayHitString = "ZoomOut";
            interactionManager.Interaction(interactionObj);
        }
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
