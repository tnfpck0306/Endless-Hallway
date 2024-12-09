using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyClassTrigger : MonoBehaviour
{
    private float timeSpentIsZone = 0f; // �÷��̾ Ʈ���� ���� ���� �ִ� �ð�
    public float triggerTime = 5f; // Ʈ���� �ߵ� �ð�
    public GameObject door; // ��ο� ���� ���� ��

    private Vector3 monsterStartPos;
    private Vector3 monsterTargetPos;
    public GameObject monster;

    private void Start()
    {
        monsterStartPos = monster.transform.localPosition;
        monsterTargetPos = new Vector3(monsterStartPos.x, monsterStartPos.y, -0.4f);
    }

    private void OnTriggerStay(Collider other)
    {
        // �÷��̾ Ʈ���� �� �ȿ� ������ ���
        if (other.CompareTag("Player"))
        {
            timeSpentIsZone += Time.deltaTime;

            if(timeSpentIsZone > triggerTime)
            {
                StartCoroutine(closeDoor(door.transform, -0.15f));
            }
        }
    }

    // �� ����
    IEnumerator closeDoor(Transform targetObject, float distance)
    {
        float elapsedTime = 0.0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;

            monster.transform.localPosition = Vector3.Lerp(monsterStartPos, monsterTargetPos, elapsedTime / 0.2f);

            if(elapsedTime > 0.8f)
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
            elapsedTime += Time.deltaTime * 0.3f;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
