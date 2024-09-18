using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{

    // 오브젝트 회전
    public void Rotation(float angle, Transform objectTransform)
    {
        float targetAngle = objectTransform.eulerAngles.y + angle;
        StartCoroutine(iRotation(targetAngle, objectTransform));
    }

    IEnumerator iRotation(float targetAngle, Transform objectTransform)
    {
        float current = objectTransform.eulerAngles.y; // 현재 위치
        float target = targetAngle; // 목표 위치
        float speed = 0f; // 회전 속도

        while (speed < 1.5f)
        {
            speed += Time.deltaTime;
            float yRoatation = Mathf.Lerp(current, target, speed) % 360.0f;

            objectTransform.eulerAngles = new Vector3(objectTransform.eulerAngles.x, yRoatation, objectTransform.eulerAngles.z);

            yield return null;
        }
    }
}
