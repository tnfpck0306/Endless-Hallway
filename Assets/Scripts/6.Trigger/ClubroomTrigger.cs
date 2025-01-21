using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문을 닫는 트리거 스크립트(문 잠금)
/// </summary>
public class ClubroomTrigger : MonoBehaviour
{
    public GameObject clubDoor; // 동아리방 문
    public GameObject findKeyTimer; // 타이머
    private bool isTriigerActive = false; // 트리거 작동 여부
    private bool isKeyActive = false; // 키 작동 여부

    public PlayerInven playerInven;

    /// <summary>
    /// 열쇠 찾기 이상현상(19번) 작동 트리거
    /// </summary>
    private void Update()
    {
        if (playerInven.blueKey && !isKeyActive)
        {
            isKeyActive = true;
            clubDoor.tag = "OpenDoor";
            findKeyTimer.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isTriigerActive)
        {
            isTriigerActive = true;
            findKeyTimer.SetActive(true);
            clubDoor.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(OpenSlide(clubDoor.transform, -0.8f, "Door")); // 문 닫기
        }
    }

    // 문 좌/우로 이동
    IEnumerator OpenSlide(Transform targetObject, float distance, string stateTag)
    {
        yield return new WaitForSeconds(1f);

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
