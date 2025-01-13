using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTrigger01 : MonoBehaviour
{
    private bool check = false;
    [SerializeField] private GameObject[] chair;
    [SerializeField] private float[] distance;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거 존 안에 들어올 경우
        if (other.CompareTag("Player") && !check)
        {
            check = true;
            for (int i = 0; i < chair.Length; i++)
            {
                StartCoroutine(Movement(chair[i].transform, distance[i]));
            }
        }
    }

    // 의자 움직임
    IEnumerator Movement(Transform targetObject, float distance)
    {
        float elapsedTime = 0.0f;
        bool soundCheck = false;

        Vector3 targetPosition;
        targetPosition = new Vector3(targetObject.localPosition.x, targetObject.localPosition.y, targetObject.localPosition.z + distance);

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            /*
            if (!soundCheck)
            {
                gameObject.GetComponent<AudioSource>().Play();
                soundCheck = true;
            }*/

            yield return null;
        }
        gameObject.SetActive(false);

    }
}
