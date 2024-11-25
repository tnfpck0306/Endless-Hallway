using System.Collections;
using UnityEngine;

public class DisappearOnLight : MonoBehaviour
{
    public Light flashLight; // 손전등 라이트
    public float detectionRange = 10f;
    public float timeToDisappear = 2f;

    [SerializeField] Transform player;
    public GameObject[] rackDoor;

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
        if(isHit)
        {
            timer += Time.deltaTime;
            if(timer >= timeToDisappear && !isTriggerActive)
            {
                isTriggerActive = true;
                StartCoroutine(Blink());
                StartCoroutine(Chase());

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

    IEnumerator Blink()
    {
        animator.SetBool("chase", true);
        flashLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = false;

        //gameObject.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z - 1.5f);
        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;
        gameObject.SetActive(false);
    }

    IEnumerator Chase()
    {


        while (true)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * 5f * Time.deltaTime;

            yield return null;
        }
    }


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
        float speed = 0.85f; // 회전 속도

        while (speed < 2.0f)
        {
            speed += Time.deltaTime;
            float yRoatation = Mathf.Lerp(current, target, speed) % 360.0f;

            objectTransform.eulerAngles = new Vector3(objectTransform.eulerAngles.x, yRoatation, objectTransform.eulerAngles.z);

            yield return null;
        }
    }
}
