using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 17번 이상현상, 복도 괴물의 움직임 트리거
/// </summary>

public class DisappearOnLight01 : MonoBehaviour
{
    public Light flashLight; // 손전등 라이트
    public float detectionRange = 10f;
    public float timeToDisappear = 2f;

    private float timer = 0f;
    private bool isHit = false; // 빛 감지 여부
    private bool isTriggerActive = false; // 트리거 작동 여부
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 라이트에서 전방으로 광선을 쏘아서 충돌 감지
        Ray ray = new Ray(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;

        // 라이트의 범위 안에서 오브젝트가 감지되면
        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            // 감지된 오브젝트가 자신인지 확인
            if (hit.collider.gameObject == gameObject)
            {
                isHit = true; // 오브젝트가 빛에 닿음
            }
            else
            {
                isHit = false; // 감지되지 않음
            }
        }
        else
        {
            isHit = false; // 라이트가 아무것도 감지하지 않음
        }

        // 빛이 시간 동안 연속적으로 오브젝트에 닿았는지 확인
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
