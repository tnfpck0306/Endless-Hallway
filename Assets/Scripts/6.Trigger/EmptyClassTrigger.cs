using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 어두운 교실(1반) 이상현상 스크립트
/// </summary>
public class EmptyClassTrigger : MonoBehaviour
{
    private float timeSpentIsZone = 0f; // 플레이어가 트리거 존에 들어와 있는 시간
    public float triggerTime = 5f; // 트리거 발동 시간
    public GameObject door; // 어두운 공간 교실 문

    private Vector3 monsterStartPos;
    private Vector3 monsterTargetPos;
    public GameObject monster;

    private bool check;
    private void Start()
    {
        monsterStartPos = monster.transform.localPosition;
        monsterTargetPos = new Vector3(monsterStartPos.x, monsterStartPos.y, -0.4f);
        check = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 트리거 존 안에 존재할 경우
        if (other.CompareTag("Player") && !check)
        {
            timeSpentIsZone += Time.deltaTime;

            if(timeSpentIsZone > triggerTime)
            {
                StartCoroutine(closeDoor(door.transform, -0.2f));
                check = true;
            }
        }
    }

    // 문 닫힘
    IEnumerator closeDoor(Transform targetObject, float distance)
    {
        float elapsedTime = 0.0f;
        bool soundCheck = false;

        while (true)
        {
            elapsedTime += Time.deltaTime;

            monster.transform.localPosition = Vector3.Lerp(monsterStartPos, monsterTargetPos, elapsedTime / 0.2f);

            if(elapsedTime > 0.6f)
            {
                monster.transform.localPosition = monsterTargetPos;
                break;
            }

            yield return null;
        }
        
        Vector3 targetPosition;
        targetPosition = new Vector3(targetObject.localPosition.x + distance, targetObject.localPosition.y, targetObject.localPosition.z);

        elapsedTime = 0.0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            if (!soundCheck)
            {
                gameObject.GetComponent<AudioSource>().Play();
                soundCheck = true;
            }

            yield return null;
        }
        gameObject.SetActive(false);
        
    }
}
