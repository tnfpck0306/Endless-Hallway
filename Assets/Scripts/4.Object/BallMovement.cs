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
    private AudioSource audioSource;
    private Collider ballCollider;
    private int count = 0;

    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ballCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (shouldMove)
        {
            if (moveTime < 2f)
            {
                moveTime += Time.deltaTime;
                // ������ ���ư��� �� ����
                rb.AddForce(Vector3.forward * fowardForce * Time.deltaTime);
            }
            else if (moveTime < 3f)
            {
                moveTime += Time.deltaTime;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (moveTime < 4f)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                moveTime += Time.deltaTime;
                if (!positionMove)
                {
                    audioSource.volume = 0.5f;
                    audioSource.clip = audioClips[1];
                    audioSource.Play();

                    // ��ġ �̵�
                    gameObject.transform.position = new Vector3(3.8f, 2.4f, 23f);
                    positionMove = true;
                }
                rb.AddForce(Vector3.right * 1500 * Time.deltaTime);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.clip = audioClips[0];
        if(audioSource.volume != 1)
        {
            audioSource.volume = 1;
        }

        // ƨ��� ��
        if (collision.gameObject.CompareTag("Untagged"))
        {
            count++;
            rb.AddForce(Vector3.up * bounceForce * Time.deltaTime);
            if (count < 9)
            {
                audioSource.Play();
            }
        }
    }

    public void startMove()
    {
        shouldMove = true;
    }
}
