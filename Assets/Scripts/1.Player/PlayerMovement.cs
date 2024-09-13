using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targetPositions; // �÷��̾� �̵��� �� ������
    public Transform[] doorObject;
    public Transform cameraTransform; // ī�޶� ��ġ ����
    public ObjectRotate objectRotate; // ������Ʈ ȸ�� ����
    public PlayerInven playerInven; // �÷��̾� �κ��丮
    public ClickManager clickManager; // �÷��̾� Ŭ�� ����
    private string rayHitString;

    public AudioSource audioSource; // �÷��̾� AudioSource
    public AudioClip footstepSound; // �÷��̾� �߼Ҹ� Clip

    public float moveSpeed = 1f; // ������ �ӵ�
    private bool isSprinting = false; // �÷��̾� �޸��� ����

    public enum PlayerState // �÷��̾��� ����
    {
        Walk, // �ȴ� ����
        Stop, // ���� ����
        ZoomIn // Ȯ�� ����
    }

    public PlayerState playerState;

    private void Start()
    {
        audioSource.clip = footstepSound;
        playerState = PlayerState.Stop;
    }

    private void Update()
    {
        if (playerInven.blueKey && rayHitString == "Anomaly")
        {
            objectRotate.Rotation(90f, doorObject[0]);
            objectRotate.Rotation(-90f, doorObject[1]);
            Movement(3);
        }
        else if (playerInven.blueKey && rayHitString == "Normal")
        {
            objectRotate.Rotation(90f, doorObject[2]);
            objectRotate.Rotation(-90f, doorObject[3]);
            Movement(4);
        }

        PlayerMove();

        if (Input.GetMouseButtonDown(0))
        {
            rayHitString = clickManager.rayHitString;
            ClickCheck();
        }
    }

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

        // �������� ���� ��
        if(movement.magnitude > 0)
        {
            audioSource.pitch = isSprinting ? 1.5f : 1f;

            playerState = PlayerState.Walk;
            if(!audioSource.isPlaying)
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

    private void Movement(int target)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPositions[target].position, 0.01f);
        Arrive(target);
    }

    private void Arrive(int target)
    {
        if (Vector3.Distance(transform.position, targetPositions[target].position) == 0)
        {
            
            if(target == 3 || target == 4)
                SceneManager.LoadScene("Endless Hallway01");

        }
    }

    private void ClickCheck()
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

    public void ChooseDoor(RaycastHit hit)
    {
        GameObject flash = transform.Find("Spot Light").gameObject;
        flash.GetComponent<AudioSource>().Play();
        flash.GetComponent<Light>().enabled = false;

        transform.position = new Vector3(21.5f, 1f, 22f);

        if (rayHitString.Equals(GameManager.instance.stageState.ToString()))
            GameManager.instance.stage++;
        else
        {
            GameManager.instance.stage = 0;
            GameManager.instance.randStage_max = 5;
        }

        GameManager.instance.GetStageState();

        //fadeControl.FadeOut();
        //fadeControl.RegisterCallback(PlayerReturn);
    }
}
