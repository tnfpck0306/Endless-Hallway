using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ݴ� Ʈ���� ��ũ��Ʈ(�� ��� X)
/// </summary>
public class CloseDoorTrigger : MonoBehaviour
{
    public GameObject door; // ��
    private bool isTriigerActive = false; // Ʈ���� �۵� ����

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isTriigerActive)
        {
            isTriigerActive = true;
            door.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(OpenSlide(door.transform, -0.8f, "OpenDoor"));
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
        gameObject.SetActive(false);
    }
}
