using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문을 닫는 트리거 스크립트(문 잠금 X)
/// </summary>
public class CloseDoorTrigger : MonoBehaviour
{
    public GameObject door; // 문
    private bool isTriigerActive = false; // 트리거 작동 여부

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isTriigerActive)
        {
            isTriigerActive = true;
            door.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(OpenSlide(door.transform, -0.8f, "OpenDoor"));
        }
    }

    // 문 좌/우로 열기
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
