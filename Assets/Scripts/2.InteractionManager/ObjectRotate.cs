using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{

    // ������Ʈ ȸ��
    public void Rotation(float angle, Transform objectTransform)
    {
        float targetAngle = objectTransform.eulerAngles.y + angle;
        StartCoroutine(iRotation(targetAngle, objectTransform));
    }

    IEnumerator iRotation(float targetAngle, Transform objectTransform)
    {
        float current = objectTransform.eulerAngles.y; // ���� ��ġ
        float target = targetAngle; // ��ǥ ��ġ
        float speed = 0f; // ȸ�� �ӵ�

        while (speed < 1.5f)
        {
            speed += Time.deltaTime;
            float yRoatation = Mathf.Lerp(current, target, speed) % 360.0f;

            objectTransform.eulerAngles = new Vector3(objectTransform.eulerAngles.x, yRoatation, objectTransform.eulerAngles.z);

            yield return null;
        }
    }
}
