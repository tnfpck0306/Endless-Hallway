using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 이상현상을 관리하는 스크립트
/// </summary>
public class AnomalyManager : MonoBehaviour
{
    public GameObject keyNotice; // 사용자 키 설명 공지문
    public GameObject[] exitLight; // 탈출구 지시등
    public GameObject[] doorText; // 탈출문 문구
    public GameObject[] doll; // 인형(0.빨강, 1.파랑, 2.초록, 3.하얀, 4.검정, 5.갈색)
    public GameObject[] hideDoll; // 숨겨진 인형(0.빨강, 1.파랑, 2.초록, 3.하얀, 4.검정, 5.갈색)
    public GameObject[] picture; // Find me 그림
    public GameObject fireExting; // 소화기
    public GameObject lockClassDoor; // 잠겨질 교실문
    public GameObject ceilingHallway; // 복도 천장
    public GameObject ceilingPart; // 복도 천장 파츠
    public GameObject ceilingMonster; // 천장 이상현상
    public GameObject airConditioner; // 동아리방 에어컨
    public GameObject ceilingClubroom; // 동아리방 천장
    public GameObject[] chairClassroom; // 교실 의자
    public GameObject[] lightClassroom; // 교실 전등
    public GameObject apple;
    public GameObject darkSpaceMonster; // 어두운 공간 유령
    public GameObject dontRunPaper; // 뛰기 금지 포스터
    public GameObject lockClubDoor; // 동아리방 문
    public GameObject[] board ; // 공지판
    public GameObject normalClassDesk; // 일반 상태 교실 책상
    public GameObject anomalyClassDesk; // 이상현상 상태 교실 책상
    public GameObject lockerRoomMonster; // 락커룸 괴물
    public GameObject speaker; // 스피커
    public GameObject poster; // 스피커 주의 포스터
    public GameObject[] lightClubroom; // 동아리방 전등
    public GameObject graffiti; // 칠판 낙서
    public GameObject anomalyGraffiti; // 칠판 낙서 이상현상
    public GameObject walkPoster; // 기본 포스터
    public GameObject posterAnomaly; // 포스터 이상현상

    public GameObject palmTrigger; // 손바닥 이상현상 트리거
    public GameObject closeDoorTrigger; // 문닫는 트리거
    public GameObject footprintTrigger; // 발자국 이상현상 트리거
    public GameObject ballbounceTrigger; // 공 굴러가는 이상현상 트리거
    public GameObject clubRoomTrigger; // 동아리방 이상현상 트리거

    public AudioManager audioManager;
    public PlayerEventAudio playerEventAudio;

    private void Start()
    {
        placeAnomaly(GameManager.instance.anomalyNum);
    }

    public void placeAnomaly(int anomalyNum)
    {
        if(GameManager.instance.stage == 0) // 0단계에서는 공지문 띄우기
            keyNotice.SetActive(true);

        switch (anomalyNum)
        {
            case 0: // 일반상태

                break;

            case 1: // 탈출구 지시등 변화 및 탈출문 문구 변화
                exitLight[0].SetActive(false);
                exitLight[1].SetActive(true);

                Vector3 indexPosition = doorText[0].transform.position;
                Quaternion indexRotation = doorText[0].transform.rotation;

                doorText[0].transform.position = doorText[1].transform.position;
                doorText[0].transform.rotation = doorText[1].transform.rotation;

                doorText[1].transform.position = indexPosition;
                doorText[1].transform.rotation = indexRotation;

                break;

            case 2: // 빨간 인형 탐색
                HideDoll(0);
                fireExting.SetActive(false);
                break;

            case 3: // 파란 인형 탐색
                HideDoll(1);
                break;

            case 4: // 초록 인형 탐색
                HideDoll(2);
                break;

            case 5: // 하얀 인형 탐색
                HideDoll(3);
                break;

            case 6: // 검은 인형 탐색
                HideDoll(4);
                break;

            case 7: // 갈색 인형 탐색
                HideDoll(5);
                break;
            
            case 8: // 피로 된 손자국
                palmTrigger.SetActive(true);
                lockClassDoor.tag = "Door";
                break;

            case 9: // 청각 상실
                audioManager.LossHearing();
                closeDoorTrigger.SetActive(true);
                break;

            case 10: // 피로 된 발자국
                footprintTrigger.SetActive(true);
                ceilingHallway.SetActive(false);
                ceilingPart.SetActive(false);
                break;

            case 11: // 동아리방 에어컨 이상현상
                ceilingMonster.SetActive(true);
                ceilingClubroom.SetActive(false);
                airConditioner.SetActive(false);
                break;

            case 12: // 교실 의자 이상현상
                Transform chairTransform;

                for(int i = 0; i < lightClassroom.Length; i++)
                    lightClassroom[i].SetActive(false);

                for (int i = 0; i < chairClassroom.Length; i++)
                {
                    chairTransform = chairClassroom[i].transform;
                    chairTransform.position = new Vector3(chairTransform.position.x, 2.95f,chairTransform.position.z);
                }
                break;

            case 13: // 어두운 교실 이상현상
                apple.SetActive(false);
                darkSpaceMonster.SetActive(true);
                break;

            case 14: // 달리기 금지 이상현상
                dontRunPaper.SetActive(true);
                for (int i = 0; i < board.Length; i++)
                {
                    board[i].SetActive(false);
                }
                lockClassDoor.tag = "Door";
                lockClubDoor.tag = "Door";
                break;
            
            case 15: // 교실 책상 수 이상현상
                normalClassDesk.SetActive(false);
                anomalyClassDesk.SetActive(true);
                break;

            case 16: // 공이 굴러가는 이상현상
                ballbounceTrigger.SetActive(true);
                break;

            case 17: // 락커룸 괴물 이상현상
                lockerRoomMonster.SetActive(true);
                break;

            case 18: // 스피커 이상현상
                poster.SetActive(true);
                SpeakerControl speakerControl = speaker.GetComponent<SpeakerControl>();
                AudioClip audioClip01 = audioManager.preloadClips[4];
                AudioClip audioClip02 = audioManager.preloadClips[8];
                speakerControl.SpeakerSound(audioClip01, audioClip02);
                break;

            case 19: // 동아리방 문 잠금 이상현상
                clubRoomTrigger.SetActive(true);
                break;

            case 20: // 동아리방 전등 이상현상

                foreach (GameObject light in lightClubroom)
                {
                    Transform lightTranform = light.transform;

                    lightTranform.rotation = Quaternion.Euler(180f, 90f, 0f);
                    lightTranform.localPosition = new Vector3(lightTranform.localPosition.x, lightTranform.localPosition.y - 0.6f, lightTranform.localPosition.z);

                }

                break;

            case 21: // 칠판 이상현상
                graffiti.SetActive(false);
                anomalyGraffiti.SetActive(true);
                break;

            case 22: // 포스터 그림 움직임 이상현상
                walkPoster.SetActive(false);
                posterAnomaly.SetActive(true);
                break;
        }
    }

    // 인형 숨기기
    private void HideDoll(int dollNum)
    {
        GameManager.instance.condition = false; // 조건 불충족
        doll[dollNum].SetActive(false);
        picture[dollNum].SetActive(true);
        hideDoll[dollNum].SetActive(true);
    }

    // 인형 복구
    public void FindeDoll(int anomalyNum)
    {
        GameManager.instance.condition = true; // 조건 충족
        int dollNum = anomalyNum - 2;

        playerEventAudio.PlayerEventSound(audioManager.preloadClips[7]);

        doll[dollNum].SetActive(true);
        picture[dollNum].SetActive(false);
        hideDoll[dollNum].SetActive(false);

    }
}
