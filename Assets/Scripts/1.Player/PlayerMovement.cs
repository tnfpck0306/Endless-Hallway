using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어의 상태와 움직임을 담당하는 스크립트
/// 이벤트에서 플레이어의 정해진 움직임 동작
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public Transform[] targetPositions; // 플레이어 이동의 각 목적지
    public Transform[] doorObject;
    public Transform cameraTransform; // 카메라 위치
    public ObjectRotate objectRotate; // 오브젝트 회전 참조
    public PlayerInven playerInven; // 플레이어 인벤토리
    public ClickManager clickManager; // 플레이어 클릭
    private string rayHitString;

    public AudioSource audioSource; // 플레이어 AudioSource
    public AudioClip footstepSound; // 플레이어 발소리 Clip

    public float moveSpeed = 1f; // 움직임 속도
    private bool isSprinting = false; // 플레이어 달리기 여부

    public enum PlayerState // 플레이어의 상태
    {
        Walk, // 걷는 시점
        Stop, // 정지 시점
        Limit, // 움직임 제한
    }
    public PlayerState playerState;

    private void Start()
    {
        audioSource.clip = footstepSound;
        playerState = PlayerState.Stop;
    }

    private void Update()
    {
        // 플레이어 움직임 제한상태가 아닐 때
        if(playerState != PlayerState.Limit)
        {
            PlayerMove(); // 플레이어 이동
        }

    }

    // 정답 선택시(이상현상 문) 플레이어 이동
    public void MoveAnomalyHall()
    {
        objectRotate.Rotation(90f, doorObject[0]);
        objectRotate.Rotation(-90f, doorObject[1]);
        StartCoroutine(Movement(3));
    }

    // 정답 선택시(일반 문) 플레이어 이동
    public void MoveNormalHall()
    {
        objectRotate.Rotation(90f, doorObject[2]);
        objectRotate.Rotation(-90f, doorObject[3]);
        StartCoroutine(Movement(4));
    }

    // 플레이어 조종 움직임
    private void PlayerMove()
    {
        // 입력값 받기
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isSprinting ? moveSpeed * 2.0f : moveSpeed;

        // 움직임 벡터 계산
        Vector3 movement = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        movement.y = 0f;

        transform.position += movement.normalized * currentSpeed * Time.deltaTime;

        StepSound(movement);
    }

    // 플레이어 정답 선택시 움직임
    IEnumerator Movement(int target)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPositions[target].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPositions[target].position) == 0)
            {

                if (target == 3 || target == 4)
                    SceneManager.LoadScene("Endless Hallway01");
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            yield return null;
        }
    }

    // 발소리
    private void StepSound(Vector3 movement)
    {
        // 움직임이 있을 때
        if (movement.magnitude > 0)
        {
            audioSource.pitch = isSprinting ? 1.5f : 1f;

            playerState = PlayerState.Walk;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            NoRunning();
        }
        // 움직임이 없을 때
        else
        {
            playerState = PlayerState.Stop;
            audioSource.Stop();
        }
    }

    // 달리기 금지(이상현상 14번)
    private void NoRunning()
    {
        if (isSprinting == true && GameManager.instance.anomalyNum == 14)
        {
            GameManager.instance.EndGame();
        }
    }
}
