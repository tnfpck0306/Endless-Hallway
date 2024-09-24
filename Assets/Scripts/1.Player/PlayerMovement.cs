using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �÷��̾��� ���¿� �������� ����ϴ� ��ũ��Ʈ
/// �̺�Ʈ���� �÷��̾��� ������ ������ ����
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public Transform[] targetPositions; // �÷��̾� �̵��� �� ������
    public Transform[] doorObject;
    public Transform cameraTransform; // ī�޶� ��ġ
    public ObjectRotate objectRotate; // ������Ʈ ȸ�� ����
    public PlayerInven playerInven; // �÷��̾� �κ��丮
    public ClickManager clickManager; // �÷��̾� Ŭ��
    private string rayHitString;

    public AudioSource audioSource; // �÷��̾� AudioSource
    public AudioClip footstepSound; // �÷��̾� �߼Ҹ� Clip

    public float moveSpeed = 1f; // ������ �ӵ�
    private bool isSprinting = false; // �÷��̾� �޸��� ����

    public enum PlayerState // �÷��̾��� ����
    {
        Walk, // �ȴ� ����
        Stop, // ���� ����
        Limit, // ������ ����
    }

    public PlayerState playerState;

    private void Start()
    {
        audioSource.clip = footstepSound;
        playerState = PlayerState.Stop;
    }

    private void Update()
    {
        // �÷��̾� ������ ���ѻ��°� �ƴ� ��
        if(playerState != PlayerState.Limit)
        {
            PlayerMove(); // �÷��̾� �̵�
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

    // �÷��̾� ������
    private void PlayerMove()
    {
        // �Է°� �ޱ�
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isSprinting ? moveSpeed * 2.0f : moveSpeed;

        // ������ ���� ���
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

    // �߼Ҹ�
    private void StepSound(Vector3 movement)
    {
        // �������� ���� ��
        if (movement.magnitude > 0)
        {
            audioSource.pitch = isSprinting ? 1.5f : 1f;

            playerState = PlayerState.Walk;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        // �������� ���� ��
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

    // �� ����
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
