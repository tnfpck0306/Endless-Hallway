using System.Collections;
using UnityEngine;

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
        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            RayClick();
            if (rayHitString != "Untagged")
            {
                Debug.Log(rayHitString);
                interactionManager.Interaction(interactionObj);
            }
        }
        // ���콺 ��Ŭ��
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
        }

    }
   
}
