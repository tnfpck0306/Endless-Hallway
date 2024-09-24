using System.Collections;
using System.Collections.Generic;
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

        if (Input.GetMouseButtonDown(0))
        {
            rayHitString = clickManager.rayHitString;
            ClickCheck();
        }

        if (playerInven.blueKey)
        {
            if (rayHitString == "Anomaly")
            {
                playerState = PlayerState.Limit;
                objectRotate.Rotation(90f, doorObject[0]);
                objectRotate.Rotation(-90f, doorObject[1]);
                Movement(3);
            }
            else if (rayHitString == "Normal")
            {
                playerState = PlayerState.Limit;
                objectRotate.Rotation(90f, doorObject[2]);
                objectRotate.Rotation(-90f, doorObject[3]);
                Movement(4);
            }
        }
    }

    // 플레이어 움직임
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

    private void Movement(int target)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPositions[target].position, moveSpeed * Time.deltaTime);
        Arrive(target);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Arrive(int target)
    {
        if (Vector3.Distance(transform.position, targetPositions[target].position) == 0)
        {
            
            if(target == 3 || target == 4)
                SceneManager.LoadScene("Endless Hallway01");

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
        }
        // 움직임이 없을 때
        else
        {
            playerState = PlayerState.Stop;
            audioSource.Stop();
        }
    }

    private void ClickCheck()
    {
        if (playerInven.blueKey)
        {
            if (rayHitString == "Anomaly")
            {
                ChooseDoor(clickManager.hit);
            }

            if (rayHitString == "Normal")
            {
                ChooseDoor(clickManager.hit);
            }
        }
    }

    // 문 선택
    public void ChooseDoor(RaycastHit hit)
    {
        GameObject flash = transform.Find("Spot Light").gameObject;
        flash.GetComponent<AudioSource>().Play();
        flash.GetComponent<Light>().enabled = false;

        transform.position = new Vector3(21.5f, 1f, 22f);

        GameManager.instance.CompareAns(rayHitString);
        GameManager.instance.GetStageState();

    }
}
