using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubroomTrigger : MonoBehaviour
{
    public GameObject clubDoor; // ���Ƹ��� ��
    private bool isTriigerActive = false; // Ʈ���� �۵� ����
    private bool isKeyActive = false; // Ű �۵� ����

    public PlayerInven playerInven;

    private void Update()
    {
        if (playerInven.blueKey && !isKeyActive)
        {
            isKeyActive = true;
            clubDoor.tag = "OpenDoor";
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isTriigerActive)
        {
            isTriigerActive = true;
            clubDoor.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(OpenSlide(clubDoor.transform, -0.8f, "Door"));
        }
    }

    // �� ��/��� ����
    IEnumerator OpenSlide(Transform targetObject, float distance, string stateTag)
    {
        Vector3 targetPosition;

        targetPosition = new Vector3(targetObject.localPosition.x + distance, targetObject.localPosition.y, targetObject.localPosition.z);

        float elapsedTime = 0.0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 0.3f;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            yield return null;
        }

        targetObject.gameObject.tag = stateTag;
    }
}
