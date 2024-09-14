using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RainMovement02 : MonoBehaviour
{
    public Transform playerTransform; // 플레이어 위치 참조
    private AudioSource audioSource; // 사운드 재생 AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(-4.0f, 1.0f, -10.0f);
    }

    void Update()
    {
        if (playerTransform.position.z > 9.0f && playerTransform.position.z < 15.0f)
        {
            transform.position = new Vector3(12.0f, 1.0f, playerTransform.position.z);
        }
        else if (playerTransform.position.z > 24.0f)
        {
            transform.position = new Vector3(playerTransform.position.x, 1.0f, 34.0f);
        }

        // 플레이어와 거리 계산
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // 거리 이내로 들어온다면 사운드 활성화/ 비활성화
        if (distance <= 5.8f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
