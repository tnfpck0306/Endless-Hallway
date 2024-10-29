using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR;

public class DisappearOnLight : MonoBehaviour
{
    public Light flashLight; // ������ ����Ʈ
    public float detectionRange = 10f;
    public float timeToDisappear = 2f;

    public GameObject[] rackDoor;

    private float timer = 0f;
    private bool isHit = false; // �� ���� ����
    private bool isTriggerActive = false; // Ʈ���� �۵� ����

    private void Update()
    {
        // ����Ʈ���� �������� ������ ��Ƽ� �浹 ����
        Ray ray = new Ray(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;

        // ����Ʈ�� ���� �ȿ��� ������Ʈ�� �����Ǹ�
        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            // ������ ������Ʈ�� �ڽ����� Ȯ��
            if (hit.collider.gameObject == gameObject)
            {
                isHit = true; // ������Ʈ�� ���� ����
            }
            else
            {
                isHit = false; // �������� ����
            }
        }
        else
        {
            isHit = false; // ����Ʈ�� �ƹ��͵� �������� ����
        }

        // ���� 2�� ���� ���������� ������Ʈ�� ��Ҵ��� Ȯ��
        if(isHit)
        {
            timer += Time.deltaTime;
            if(timer >= timeToDisappear && !isTriggerActive)
            {
                isTriggerActive = true;
                StartCoroutine(blink());

                foreach (GameObject rackObject in rackDoor)
                {
                    Rotation(90f, rackObject.transform);
                }
            }
        }
        else
        {
            timer = 0f;
        }
    }

    IEnumerator blink()
    {
        flashLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = false;

        gameObject.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z - 1.5f);
        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;
        gameObject.SetActive(false);
    }


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
        float speed = 0.85f; // ȸ�� �ӵ�

        while (speed < 2.0f)
        {
            speed += Time.deltaTime;
            float yRoatation = Mathf.Lerp(current, target, speed) % 360.0f;

            objectTransform.eulerAngles = new Vector3(objectTransform.eulerAngles.x, yRoatation, objectTransform.eulerAngles.z);

            yield return null;
        }
    }
}
