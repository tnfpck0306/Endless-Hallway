using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targetPositions; // 플레이어 이동의 각 목적지
    public Transform[] doorObject;
    public Transform cameraTransform;
    public PlayerRotate playerRotate;
    public FadeControl fadeControl;
    public ObjectRotate objectRotate;
    public ClickManager clickManager;

    public float playerSpeed;
    private string rayHitString;
    public bool doorCheck;

    public float moveSpeed = 1f; // 움직임 속도

    public AudioSource audioSource;
    public AudioClip footstepSound;

    // 플레이어가 완료한 도착 위치 (0. 초기위치, 1. 코너위치, 2. 도착위치)
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
        // 입력값 받기
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 움직임 벡터 계산
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

    // 한사이클 후 복귀
    public void PlayerReturn()
    {
        transform.eulerAngles = new Vector3(0, 0, 0); // 플레이어 Rotate 초기화
        transform.position = targetPositions[0].position; // 플레이어 Position 초기화
        
        playerRotate.cornercheck = false; // 코너 시점변경 확인 초기화
        playerRotate.check = true; // 플레이어 시점 변화 초기화

        fadeControl.FadeIn(); // 페이드인
        playerLocation = 0; // 위치 초기화
        playerRotate.playerState = PlayerRotate.rotateState.Stop; // 플레이어 Stop 상태
    }
}
