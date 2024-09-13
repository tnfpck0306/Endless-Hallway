using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMovement : MonoBehaviour
{
    public Transform playerTransform; // �÷��̾� ��ġ ����
    private AudioSource audioSource; // ���� ��� AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(-4.0f, 1.0f, -10.0f);
    }
    // -10, 13
    void Update()
    {
        if (playerTransform.position.z > -10.0f && playerTransform.position.z < 13.0f)
        {
            transform.position = new Vector3(-4.0f, 1.0f, playerTransform.position.z);
        }

        // �÷��̾�� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // �Ÿ� �̳��� ���´ٸ� ���� Ȱ��ȭ/ ��Ȱ��ȭ
        if (distance <= 5.8f)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
