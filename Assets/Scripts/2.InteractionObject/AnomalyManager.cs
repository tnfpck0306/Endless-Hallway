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
    public GameObject eraseExit; // Ż�ⱸ �׸� ����
    public GameObject[] doll; // ����(0.����, 1.�Ķ�, 2.�ʷ�, 3.�Ͼ�, 4.����, 5.����)
    public GameObject[] hideDoll; // ������ ����(0.����, 1.�Ķ�, 2.�ʷ�, 3.�Ͼ�, 4.����, 5.����)
    public GameObject dollAnim; // ���ӿ����� ���Ǵ� ���� ������Ʈ
    public GameObject[] picture; // Find me �׸�
    public GameObject fireExting; // ��ȭ��
    public GameObject lockClassDoor; // ����� ���ǹ�
    public GameObject ceilingHallway; // ���� õ��
    public GameObject ceilingPart; // ���� õ�� ����
    public GameObject ceilingMonster; // õ�� �̻�����
    public GameObject airConditioner; // ���Ƹ��� ������
    public GameObject ceilingClubroom; // ���Ƹ��� õ��
    public GameObject[] chairClassroom; // ���� ����
    public GameObject[] lightClassroom; // ���� ����
    public GameObject apple;
    public GameObject darkSpaceMonster; // ��ο� ���� ����
    public GameObject dontRunPaper; // �ٱ� ���� ������
    public GameObject lockClubDoor; // ���Ƹ��� ��
    public GameObject[] board ; // ������
    public GameObject normalClassDesk; // �Ϲ� ���� ���� å��
    public GameObject anomalyClassDesk; // �̻����� ���� ���� å��
    public GameObject lockerRoomMonster; // ��Ŀ�� ����
    public GameObject speaker; // ����Ŀ
    public GameObject poster; // ����Ŀ ���� ������
    public GameObject[] lightClubroom; // ���Ƹ��� ����
    public GameObject graffiti; // ĥ�� ����
    public GameObject anomalyGraffiti; // ĥ�� ���� �̻�����
    public GameObject walkPoster; // �⺻ ������
    public GameObject posterAnomaly; // ������ �̻�����

    public bool ceilingAnomalyOn = false; // õ�� ��ǰ �̻����� �۵� ����


    public Transform[] teleportPosition; // �����̵� ��ġ
    public Transform playerTransform; // �÷��̾� ��ġ
    public Light spotLight; // ������

    public GameObject palmTrigger; // �չٴ� �̻����� Ʈ����
    public GameObject closeDoorTrigger; // ���ݴ� Ʈ����
    public GameObject footprintTrigger; // ���ڱ� �̻����� Ʈ����
    public GameObject ballbounceTrigger; // �� �������� �̻����� Ʈ����
    public GameObject clubRoomTrigger; // ���Ƹ��� �̻����� Ʈ����

    public AudioManager audioManager;
    public PlayerMovement playerMovement;
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

            case 1: // Ż�ⱸ ���õ� ��ȭ �� Ż�⹮ ���� ��ȭ
                exitLight[0].SetActive(false);
                exitLight[1].SetActive(true);
                eraseExit.SetActive(true);

                break;

            case 2: // ���� ���� Ž��
                HideDoll(0);
                fireExting.SetActive(false);
                StartCoroutine(Timer(5));
                break;

            case 3: // �Ķ� ���� Ž��
                HideDoll(1);
                StartCoroutine(Timer(200));
                break;

            case 4: // �ʷ� ���� Ž��
                HideDoll(2);
                StartCoroutine(Timer(200));
                break;

            case 5: // �Ͼ� ���� Ž��
                HideDoll(3);
                StartCoroutine(Timer(200));
                break;

            case 6: // ���� ���� Ž��
                HideDoll(4);
                StartCoroutine(Timer(200));
                break;

            case 7: // ���� ���� Ž��
                HideDoll(5);
                StartCoroutine(Timer(200));
                break;
            
            case 8: // �Ƿ� �� ���ڱ�
                palmTrigger.SetActive(true);
                lockClassDoor.tag = "Door";
                break;

            case 9: // û�� ���
                audioManager.LossHearing();
                closeDoorTrigger.SetActive(true);
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

            case 12: // ���� ���� �̻�����
                Transform chairTransform;

                for(int i = 0; i < lightClassroom.Length; i++)
                    lightClassroom[i].SetActive(false);

                for (int i = 0; i < chairClassroom.Length; i++)
                {
                    chairTransform = chairClassroom[i].transform;
                    chairTransform.position = new Vector3(chairTransform.position.x, 2.95f,chairTransform.position.z);
                }
                break;

            case 13: // ��ο� ����(1��) �̻�����
                apple.SetActive(false);
                darkSpaceMonster.SetActive(true);
                break;

            case 14: // �޸��� ���� �̻�����
                // PlayerMovement�� �̻����� ���� ��ũ��Ʈ
                dontRunPaper.SetActive(true);
                for (int i = 0; i < board.Length; i++)
                {
                    board[i].SetActive(false);
                }
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
                // PlayerMovement�� �̻����� ���� ��ũ��Ʈ

                poster.SetActive(true);
                SpeakerControl speakerControl = speaker.GetComponent<SpeakerControl>();
                AudioClip audioClip01 = audioManager.preloadClips[4];
                AudioClip audioClip02 = audioManager.preloadClips[8];
                speakerControl.SpeakerSound(audioClip01, audioClip02);
                break;

            case 19: // ���Ƹ��� ���� ã�� �̻�����
                clubRoomTrigger.SetActive(true);
                break;

            case 20: // ���Ƹ��� ���� �̻�����

                foreach (GameObject light in lightClubroom)
                {
                    Transform lightTranform = light.transform;

                    lightTranform.rotation = Quaternion.Euler(180f, 90f, 0f);
                    lightTranform.localPosition = new Vector3(lightTranform.localPosition.x, lightTranform.localPosition.y - 0.6f, lightTranform.localPosition.z);

                }

                break;

            case 21: // ĥ�� �̻�����
                graffiti.SetActive(false);
                anomalyGraffiti.SetActive(true);
                break;

            case 22: // ������ �׸� ������ �̻�����
                walkPoster.SetActive(false);
                posterAnomaly.SetActive(true);
                break;

            case 23: // ���� Ż�� �̻�����
                break;

            case 24: // õ�� ��ǰ �̻�����
                ceilingAnomalyOn = true;
                break;

            case 25: // �޴�â �̻�����
                // UIManger�� �̻����� ���� ��ũ��Ʈ
                // Camera control�� �̻����� ���� ��ũ��Ʈ

                break;

            case 26: // ������ �̻�����
                StartCoroutine(Teleport());
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

        playerEventAudio.PlayerEventSound(audioManager.preloadClips[7]);

        doll[dollNum].SetActive(true);
        picture[dollNum].SetActive(false);
        hideDoll[dollNum].SetActive(false);

    }

    // �ð� ���� Ÿ�̸�
    IEnumerator Timer(float time)
    {
        float currentTime = 0;

        while(true)
        {
            currentTime += Time.deltaTime;

            if(currentTime > time)
            {
                // �ð��ȿ� ���� ������ �� ���ӿ���
                if (!GameManager.instance.condition)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Limit;
                    GameManager.instance.EndGame();
                    dollAnim.SetActive(true);
                }
                break;
            }

            yield return null;
        }
    }

    // 26�� �̻�����(�����̵�)
    IEnumerator Teleport()
    {
        float currentTime = 0;
        int randPosition;

        AudioSource audioSource = spotLight.GetComponent<AudioSource>();
        SpotLightAudio spotLightAudio = spotLight.GetComponent<SpotLightAudio>();

        while (true)
        {
            currentTime += Time.deltaTime; 

            if(currentTime > 25)
            {
                audioSource.clip = spotLightAudio.audioClip[1];
                audioSource.Play();

                spotLight.enabled = false;
                yield return new WaitForSeconds(0.15f);
                spotLight.enabled = true;
                yield return new WaitForSeconds(0.3f);
                spotLight.enabled = false;
                yield return new WaitForSeconds(0.15f);
                spotLight.enabled = true;
                yield return new WaitForSeconds(2f);

                spotLight.enabled = false;
                randPosition = Random.Range(0, teleportPosition.Length);

                yield return new WaitForSeconds(1f);
                audioSource.clip = spotLightAudio.audioClip[2];
                audioSource.Play();
                playerTransform.position = teleportPosition[randPosition].position;
                playerTransform.rotation = teleportPosition[randPosition].rotation;

                yield return new WaitForSeconds(1f);
                audioSource.clip = spotLightAudio.audioClip[0];
                audioSource.Play();
                spotLight.enabled = true;
                currentTime = 0;
            }

            yield return null;
        }
    }
}
