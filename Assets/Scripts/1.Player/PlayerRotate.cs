using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRotate : MonoBehaviour
{
    private Transform playerTransform;
    public PlayerMovement playerMovement;

    public Text playerStateText;
    public Text checkText;

    public bool check; // 시점 변화 가능 여부 (True = 시점 변화 가능, False = 시점 변화 불가능)
    public bool cornercheck = false; // 코너에서 시점 변화 완료 체크

    public enum rotateState // 플레이어의 시점 상태
    {
        Walk, // 걷는 시점
        Stop, // 정지 시점
        Back // 뒤쪽 시점
    }

    public rotateState playerState;
    private rotateState prevState;

    private void Start()
    {
        playerState = rotateState.Stop;
        playerTransform = gameObject.transform;
        check = true;
    }

    private void Update()
    {
        playerStateText.text = $"PlayerState : {playerState}";
        checkText.text = $"TurnCheck : {check}";

        // zoom-In 상태가 아닐 때 플레이어는 상태를 바꿀 수 있다.
        if (!GameManager.instance.zoomIn)
        {

            // 플레이어 정지
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (playerState != rotateState.Stop)
                    playerState = rotateState.Stop;
                else
                    playerState = rotateState.Walk;
            }

            // 플레이어가 뒤돌때
            if (Input.GetKeyDown(KeyCode.Space) && check)
            {
                check = false;
                if (playerState == rotateState.Walk)
                {
                    Rotation(180f);
                    prevState = rotateState.Walk;
                    playerState = rotateState.Back;
                }

                else if (playerState == rotateState.Stop)
                {
                    Rotation(180f);
                    prevState = rotateState.Stop;
                    playerState = rotateState.Back;
                }

                else if (prevState == rotateState.Walk)
                {
                    Rotation(-180f);
                    playerState = rotateState.Walk;
                }

                else if (prevState == rotateState.Stop)
                {
                    Rotation(-180f);
                    playerState = rotateState.Stop;
                }
            }

        }
    }

    public void Rotation(float angle)
    {
        float targetAngle = playerTransform.eulerAngles.y + angle;
        StartCoroutine(iRotation(targetAngle));
    }

    IEnumerator iRotation(float targetAngle)
    {
        float current = playerTransform.eulerAngles.y;
        float target = targetAngle;

        float speed = 0f;

        while (speed < 1.5f)
        {
            speed += Time.deltaTime;
            float yRoatation = Mathf.Lerp(current, target, speed) % 360.0f;

            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, yRoatation, playerTransform.eulerAngles.z);

            yield return null;
        }
        check = true;

        if (playerMovement.playerLocation == 1)
        {
            yield return new WaitForSeconds(1f);
            cornercheck = true;
            playerState = rotateState.Walk;
        }
    }
}
