using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 17�� �̻�����, ���� ������ ������ Ʈ����
/// </summary>

public class DisappearOnLight01 : MonoBehaviour
{
    public Light flashLight; // ������ ����Ʈ
    public float detectionRange = 10f;
    public float timeToDisappear = 2f;

    private float timer = 0f;
    private bool isHit = false; // �� ���� ����
    private bool isTriggerActive = false; // Ʈ���� �۵� ����
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

        // ���� �ð� ���� ���������� ������Ʈ�� ��Ҵ��� Ȯ��
        if (isHit)
        {
            timer += Time.deltaTime;
            if (timer >= timeToDisappear && !isTriggerActive)
            {
                isTriggerActive = true;
                animator.SetBool("Found", true);

            }
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Standing02"))
        {
            gameObject.SetActive(false);
        }

    }

}
