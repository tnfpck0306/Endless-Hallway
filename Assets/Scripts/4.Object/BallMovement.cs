using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float fowardForce = 10f; // 앞으로 나아가는 힘
    public float bounceForce = 5f; // 튕길 때의 힘
    private bool shouldMove = false; // 움직임 이벤트 여부

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(shouldMove)
        // 앞으로 나아가는 힘 적용
        rb.AddForce(Vector3.forward * fowardForce * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
