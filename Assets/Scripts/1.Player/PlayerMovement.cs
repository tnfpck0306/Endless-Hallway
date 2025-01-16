using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public SpeakerControl speakerControl; // ����Ŀ ��ȣ�ۿ�

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

    }

    // ���� ���ý�(�̻����� ��) �÷��̾� �̵�
    public void MoveAnomalyHall()
    {
        objectRotate.Rotation(90f, doorObject[0]);
        objectRotate.Rotation(-90f, doorObject[1]);
        StartCoroutine(Movement(0));
    }

    // ���� ���ý�(�Ϲ� ��) �÷��̾� �̵�
    public void MoveNormalHall()
    {
        objectRotate.Rotation(90f, doorObject[2]);
        objectRotate.Rotation(-90f, doorObject[3]);
        StartCoroutine(Movement(1));
    }

    // �÷��̾� ���� ������
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

    // �÷��̾� ���� ���ý� ������
    IEnumerator Movement(int target)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPositions[target].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPositions[target].position) == 0)
            {

                // ��ǥ ���� ������
                if (target == 0 || target == 1)
                {
                    GameManager.instance.ChangeScean();

                }
                
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            yield return null;
        }
    }

    // �߼Ҹ�
    private void StepSound(Vector3 movement)
    {
        // �������� ���� ��
        if (movement.magnitude > 0)
        {
            audioSource.pitch = isSprinting ? moveSpeed * 0.75f : moveSpeed * 0.5f;

            playerState = PlayerState.Walk;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            NoRunning();
            NoMovement();
        }
        // �������� ���� ��
        else
        {
            playerState = PlayerState.Stop;
            audioSource.Stop();
        }
    }

    // �޸��� ����(�̻����� 14��)
    private void NoRunning()
    {
        if (isSprinting == true && GameManager.instance.anomalyNum == 14)
        {
            GameManager.instance.EndGame();
        }
    }

    private void NoMovement()
    {
        // ����Ŀ �Ҹ��� �鸮�� ���� �� ������ ����(18�� �̻�����)
        if (GameManager.instance.anomalyNum == 18)
        {
            if (playerState == PlayerState.Walk && !speakerControl.isPlayingSound)
                GameManager.instance.EndGame();
        }
    }
}
