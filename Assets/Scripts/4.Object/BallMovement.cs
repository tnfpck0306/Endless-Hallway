using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float fowardForce = 10f; // ������ ���ư��� ��
    public float bounceForce = 5f; // ƨ�� ���� ��
    private bool shouldMove = false; // ������ �̺�Ʈ ����

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(shouldMove)
        // ������ ���ư��� �� ����
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
