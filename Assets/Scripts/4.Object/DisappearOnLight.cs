using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR;

public class DisappearOnLight : MonoBehaviour
{
    public Light flashLight; // ������ ����Ʈ
    public float detectionRange = 10f;
    public float timeToDisappear = 2f;

    private float timer = 0f;
    private bool isHit = false;

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
            if(timer >= timeToDisappear)
            {
                StartCoroutine(blink());
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

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;
        gameObject.SetActive(false);
    }

}
