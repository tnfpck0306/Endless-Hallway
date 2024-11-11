using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CeilingMonsterMovement : MonoBehaviour
{
    public Light flashLight; // 손전등 라이트
    [SerializeField] private float detectionRange = 90f; // 감지 범위
    [SerializeField] private float timeToDisappear = 1f; // 작동 시간

    private Rigidbody rb;
    [SerializeField] private float force = 10f;
    private float moveTime = 0f;

    private float timer = 0f;
    private bool isHit = false; // 빛 감지 여부
    private bool isTriggerActive = false; // 트리거 작동 여부

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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

        // 빛이 2초 동안 연속적으로 오브젝트에 닿았는지 확인
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
            // 앞으로 나아가는 힘 적용
            rb.AddForce(Vector3.right * force * Time.deltaTime);

            yield return null;
        }
    }
}
