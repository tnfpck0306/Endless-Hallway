using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targetPositions; // �÷��̾� �̵��� �� ������
    public Transform[] doorObject;
    public Transform cameraTransform;
    public PlayerRotate playerRotate;
    public FadeControl fadeControl;
    public ObjectRotate objectRotate;
    public ClickManager clickManager;

    public float playerSpeed;
    private string rayHitString;
    public bool doorCheck;

    public float moveSpeed = 1f; // ������ �ӵ�

    public AudioSource audioSource;
    public AudioClip footstepSound;

    // �÷��̾ �Ϸ��� ���� ��ġ (0. �ʱ���ġ, 1. �ڳ���ġ, 2. ������ġ)
    public int playerLocation = 0;

    private void Start()
    {
        audioSource.clip = footstepSound;
    }

    private void Update()
    {
        /*
        if (playerRotate.playerState == PlayerRotate.rotateState.Walk)
        {
            if (playerLocation == 0)
            {
                Movement(1);
            }
            else if (playerLocation == 1 && playerRotate.cornercheck)
            {
                Movement(2);
            }
            else if (playerLocation == 2 && rayHitString == "Anomaly")
            {
                objectRotate.Rotation(90f, doorObject[0]);
                objectRotate.Rotation(-90f, doorObject[1]);
                Movement(3);
            }
            else if (playerLocation == 2 && rayHitString == "Normal")
            {
                objectRotate.Rotation(90f, doorObject[2]);
                objectRotate.Rotation(-90f, doorObject[3]);
                Movement(4);
            }
        }*/

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

        // ������ ���� ���
        Vector3 movement = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        movement.y = 0f;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;

        if(movement.magnitude > 0)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Movement(int target)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPositions[target].position, playerSpeed);
        Arrive(target);
    }

    private void Arrive(int target)
    {
        if (Vector3.Distance(transform.position, targetPositions[target].position) == 0)
        {
            playerRotate.playerState = PlayerRotate.rotateState.Stop;
            
            if(target == 3 || target == 4)
                SceneManager.LoadScene("Endless Hallway01");

        }
    }

    private void ClickCheck()
    {
        if (rayHitString == "Key")
        {
            clickManager.ZoomOut();
            playerRotate.check = false;
            playerLocation = 1;
            playerRotate.Rotation(90f);
        }

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

        playerRotate.playerState = PlayerRotate.rotateState.Walk;
        transform.position = new Vector3(21.5f, 1f, 22f);
        playerSpeed = 0.01f;

        if (rayHitString.Equals(GameManager.instance.stageState.ToString()))
            GameManager.instance.stage++;
        else
        {
            GameManager.instance.stage = 0;
            GameManager.instance.randStage_max = 5;
        }

        GameManager.instance.GetStageState();
        playerLocation = 2;

        //fadeControl.FadeOut();
        //fadeControl.RegisterCallback(PlayerReturn);
    }

    // �ѻ���Ŭ �� ����
    public void PlayerReturn()
    {
        transform.eulerAngles = new Vector3(0, 0, 0); // �÷��̾� Rotate �ʱ�ȭ
        transform.position = targetPositions[0].position; // �÷��̾� Position �ʱ�ȭ
        
        playerRotate.cornercheck = false; // �ڳ� �������� Ȯ�� �ʱ�ȭ
        playerRotate.check = true; // �÷��̾� ���� ��ȭ �ʱ�ȭ

        fadeControl.FadeIn(); // ���̵���
        playerLocation = 0; // ��ġ �ʱ�ȭ
        playerRotate.playerState = PlayerRotate.rotateState.Stop; // �÷��̾� Stop ����
    }
}
