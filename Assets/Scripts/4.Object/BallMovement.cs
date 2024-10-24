using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float fowardForce = 10f; // ������ ���ư��� ��
    public float bounceForce = 5f; // ƨ�� ���� ��
    private bool shouldMove = false; // ������ �̺�Ʈ ����
    private float moveTime = 0; // ������ �ð�
    private bool positionMove = false; // ��ġ �̵� ����

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
                // ������ ���ư��� �� ����
                rb.AddForce(Vector3.forward * fowardForce * Time.deltaTime);
            }
            else if (moveTime < 3.4f)
            {
                moveTime += Time.deltaTime;
                if (!positionMove)
                {
                    // ��ġ �̵�
                    gameObject.transform.position = new Vector3(10.6f, 1.6f, 25f);
                    positionMove = true;
                }
                rb.AddForce(Vector3.back * 1000 * Time.deltaTime);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ƨ��� ��
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
