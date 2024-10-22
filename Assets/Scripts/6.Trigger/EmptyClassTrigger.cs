using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyClassTrigger : MonoBehaviour
{
    private float timeSpentIsZone = 0f; // 플레이어가 트리거 존에 들어와 있는 시간
    public float triggerTime = 5f; // 트리거 발동 시간
    public GameObject door; // 어두운 공간 교실 문

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 트리거 존 안에 존재할 경우
        if (other.CompareTag("Player"))
        {
            timeSpentIsZone += Time.deltaTime;

            if(timeSpentIsZone > triggerTime)
            {
                StartCoroutine(closeDoor(door.transform, -0.15f));
            }
        }
    }

    // 문 닫힘
    IEnumerator closeDoor(Transform targetObject, float distance)
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
        gameObject.SetActive(false);
    }
}
