using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float fowardForce = 10f; // 앞으로 나아가는 힘
    public float bounceForce = 5f; // 튕길 때의 힘
    private bool shouldMove = false; // 움직임 이벤트 여부
    private float moveTime = 0; // 움직임 시간
    private bool positionMove = false; // 위치 이동 여부

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (shouldMove)
        {
            if (moveTime < 3f)
            {
                moveTime += Time.deltaTime;
                // 앞으로 나아가는 힘 적용
                rb.AddForce(Vector3.forward * fowardForce * Time.deltaTime);
            }
            else if (moveTime < 3.4f)
            {
                moveTime += Time.deltaTime;
                if (!positionMove)
                {
                    // 위치 이동
                    gameObject.transform.position = new Vector3(10.6f, 1.6f, 25f);
                    positionMove = true;
                }
                rb.AddForce(Vector3.back * 1000 * Time.deltaTime);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 튕기는 힘
        if (collision.gameObject.CompareTag("Untagged"))
        {
            rb.AddForce(Vector3.up * bounceForce * Time.deltaTime);
        }
    }

    public void startMove()
    {
        shouldMove = true;
    }
}
