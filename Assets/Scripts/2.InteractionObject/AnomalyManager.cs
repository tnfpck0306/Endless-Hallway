using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �̻������� �����ϴ� ��ũ��Ʈ
/// </summary>
public class AnomalyManager : MonoBehaviour
{
    public GameObject keyNotice; // ����� Ű ���� ������
    public GameObject[] exitLight; // Ż�ⱸ ���õ�
    public GameObject[] doll; // ����(0.����, 1.�Ķ�, 2.�ʷ�, 3.�Ͼ�, 4.����, 5.����)
    public GameObject[] hideDoll; // ������ ����(0.����, 1.�Ķ�, 2.�ʷ�, 3.�Ͼ�, 4.����, 5.����)
    public GameObject[] picture; // Find me �׸�
    public GameObject fireExting; // ��ȭ��
    public GameObject lockClassDoor; // ����� ���ǹ�
    public GameObject ceilingHallway; // ���� õ��
    public GameObject ceilingPart; // ���� õ�� ����
    public GameObject ceilingMonster; // õ�� �̻�����
    public GameObject airConditioner; // ���Ƹ��� ������
    public GameObject ceilingClubroom; // ���Ƹ��� õ��
    public GameObject[] rackClassroom; // ���� �繰��
    public GameObject darkSpaceMonster; // ��ο� ���� ����
    public GameObject dontRunPaper; // �ٱ� ���� ������
    public GameObject lockClubDoor; // ���Ƹ��� ��
    public GameObject notice; // ������
    public GameObject normalClassDesk; // �Ϲ� ���� ���� å��
    public GameObject anomalyClassDesk; // �̻����� ���� ���� å��
    public GameObject lockerRoomMonster; // ��Ŀ�� ����
    public GameObject speaker; // ����Ŀ

    public GameObject palmTrigger; // �չٴ� �̻����� Ʈ����
    public GameObject footprintTrigger; // ���ڱ� �̻����� Ʈ����
    public GameObject ballbounceTrigger; // �� �������� �̻����� Ʈ����
    public GameObject clubRoomTrigger; // ���Ƹ��� �̻����� Ʈ����

    public AudioManager audioManager;
    public PlayerEventAudio playerEventAudio;

    private void Start()
    {
        placeAnomaly(GameManager.instance.anomalyNum);
    }

    public void placeAnomaly(int anomalyNum)
    {
        if(GameManager.instance.stage == 0) // 0�ܰ迡���� ������ ����
            keyNotice.SetActive(true);

        switch (anomalyNum)
        {
            case 0: // �Ϲݻ���
                break;

            case 1: // Ż�ⱸ ���õ� ��ȭ
                exitLight[0].SetActive(false);
                exitLight[1].SetActive(true);
                break;

            case 2: // ���� ���� Ž��
                HideDoll(0);
                fireExting.SetActive(false);
                break;

            case 3: // �Ķ� ���� Ž��
                HideDoll(1);
                break;

            case 4: // �ʷ� ���� Ž��
                HideDoll(2);
                break;

            case 5: // �Ͼ� ���� Ž��
                HideDoll(3);
                break;

            case 6: // ���� ���� Ž��
                HideDoll(4);
                break;

            case 7: // ���� ���� Ž��
                HideDoll(5);
                break;
            
            case 8: // �Ƿ� �� ���ڱ�
                palmTrigger.SetActive(true);
                lockClassDoor.tag = "Door";
                break;

            case 9: // û�� ���
                audioManager.LossHearing();
                break;

            case 10: // �Ƿ� �� ���ڱ�
                footprintTrigger.SetActive(true);
                ceilingHallway.SetActive(false);
                ceilingPart.SetActive(false);
                break;

            case 11: // ���Ƹ��� ������ �̻�����
                ceilingMonster.SetActive(true);
                ceilingClubroom.SetActive(false);
                airConditioner.SetActive(false);
                break;

            case 12: // ���� �繰�� �̻�����
                for(int i = 0; i < rackClassroom.Length; i++)
                {
                    rackClassroom[i].SetActive(false);
                }
                break;

            case 13: // ��ο� ���� �̻�����
                darkSpaceMonster.SetActive(true);
                break;

            case 14: // �޸��� ���� �̻�����
                dontRunPaper.SetActive(true);
                notice.SetActive(false);
                lockClassDoor.tag = "Door";
                lockClubDoor.tag = "Door";
                break;
            
            case 15: // ���� å�� �� �̻�����
                normalClassDesk.SetActive(false);
                anomalyClassDesk.SetActive(true);
                break;

            case 16: // ���� �������� �̻�����
                ballbounceTrigger.SetActive(true);
                break;

            case 17: // ��Ŀ�� ���� �̻�����
                lockerRoomMonster.SetActive(true);
                break;

            case 18: // ����Ŀ �̻�����
                SpeakerControl speakerControl = speaker.GetComponent<SpeakerControl>();
                AudioClip audioClip = audioManager.preloadClips[4];
                speakerControl.SpeakerSound(audioClip);
                break;

            case 19: // ���Ƹ��� �� ��� �̻�����
                clubRoomTrigger.SetActive(true);
                break;
        }
    }

    // ���� �����
    private void HideDoll(int dollNum)
    {
        GameManager.instance.condition = false; // ���� ������
        doll[dollNum].SetActive(false);
        picture[dollNum].SetActive(true);
        hideDoll[dollNum].SetActive(true);
    }

    // ���� ����
    public void FindeDoll(int anomalyNum)
    {
        GameManager.instance.condition = true; // ���� ����
        int dollNum = anomalyNum - 2;

        playerEventAudio.DollLaughSound(audioManager.preloadClips[7]);

        doll[dollNum].SetActive(true);
        picture[dollNum].SetActive(false);
        hideDoll[dollNum].SetActive(false);

    }
}
