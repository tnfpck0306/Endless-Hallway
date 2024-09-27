using System.Collections;
using UnityEngine;

/// <summary>
/// ���콺 Ŭ���� ���� ������Ʈ �ױ׸� �ν��ϰ� �ٸ� ��ũ��Ʈ�鿡�� �˸��� ����
/// </summary>
public class ClickManager : MonoBehaviour
{
    public InteractionManager interactionManager;
    public GameObject player;
    public GameObject InteratctionReticle;

    public Ray ray;
    public RaycastHit hit;
    public GameObject interactionObj; // ��ȣ�ۿ� �ϴ� ������Ʈ
    public string rayHitString; // ��ȣ�ۿ� �ϴ� ������Ʈ�� �ױ�'string'

    private void Start()
    {
        // Ŀ���� ȭ�� �߰��� ����, Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RayPoint();
        float distance = Vector3.Distance(player.transform.position, interactionObj.transform.position);
        
        // ��ȣ�ۿ� ������ ������Ʈ�� ���� �Ÿ� �ȿ� ���� ��
        if (rayHitString != "Untagged" && distance < 4.0f)
        {
            InteratctionReticle.SetActive(true); // ��ȣ�ۿ� ���������� ui Ȱ��ȭ

            // ���콺 ��Ŭ��
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(rayHitString);
                interactionManager.Interaction(interactionObj);
            }
        }
        else
        {
            InteratctionReticle.SetActive(false); // ��ȣ�ۿ� ���������� ui ��Ȱ��ȭ
        }

        // ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(1))
        {
            rayHitString = "ZoomOut";
            interactionManager.Interaction(interactionObj);
        }
    }

    // Ŀ�� ��ġ�� �����ϴ� ������Ʈ Ȯ��
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
