using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CeilingMonsterMovement : MonoBehaviour
{
    public Light flashLight; // ������ ����Ʈ
    [SerializeField] private float detectionRange = 90f; // ���� ����
    [SerializeField] private float timeToDisappear = 1f; // �۵� �ð�

    private Rigidbody rb;
    [SerializeField] private float force = 10f;
    private float moveTime = 0f;

    private float timer = 0f;
    private bool isHit = false; // �� ���� ����
    private bool isTriggerActive = false; // Ʈ���� �۵� ����

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

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
        if (isHit)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDisappear && !isTriggerActive)
            {
                isTriggerActive = true;
                StartCoroutine(Movement());

            }
        }
        else
        {
            timer = 0f;
        }
    }

    IEnumerator Movement()
    {
        while (moveTime < 3f)
        {
            moveTime += Time.deltaTime;
            // ������ ���ư��� �� ����
            rb.AddForce(Vector3.right * force * Time.deltaTime);

            yield return null;
        }
    }
}
