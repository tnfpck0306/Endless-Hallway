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

    public bool check; // ���� ��ȭ ���� ���� (True = ���� ��ȭ ����, False = ���� ��ȭ �Ұ���)
    public bool cornercheck = false; // �ڳʿ��� ���� ��ȭ �Ϸ� üũ

    public enum rotateState // �÷��̾��� ���� ����
    {
        Walk, // �ȴ� ����
        Stop, // ���� ����
        Back // ���� ����
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

        // zoom-In ���°� �ƴ� �� �÷��̾�� ���¸� �ٲ� �� �ִ�.
        if (!GameManager.instance.zoomIn)
        {

            // �÷��̾� ����
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (playerState != rotateState.Stop)
                    playerState = rotateState.Stop;
                else
                    playerState = rotateState.Walk;
            }

            // �÷��̾ �ڵ���
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
