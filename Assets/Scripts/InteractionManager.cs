using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject flashLight;
    public GameObject zoomOutButton;

    public Cameracontrol cameraControl; // ī�޶� ������ ����
    private ObjectRotate objectRotate;
    public ClickManager clickManager;
    public PlayerMovement playerMovement;
    public PlayerInven playerInven;
    public AudioManager audioManager;
    private AudioSource audioSource;

    [SerializeField] private float shakeTime = 0.6f;
    [SerializeField] private float shakeSpeed = 1.0f;
    [SerializeField] private float shakeAmount = 0.5f;

    private Transform TargetObject;
    private AudioSource TargetAudio;
    private Vector3 cameraPosition; // Zoom-In ���� ī�޶� ��ġ
    private Vector3 flashPosition; // Zoom-In ���� ������ ��ġ

    private float time; // �ð� ����

    public void Start()
    {
        objectRotate = gameObject.GetComponent<ObjectRotate>();
        
        time = 0;
        StartCoroutine(CheckTime());
    }

    public void Interaction(GameObject interactionObj)
    {
        string targetTag = clickManager.rayHitString;
        switch (targetTag)
        {
            // ���� ȹ��
            case "Key":
                playerInven.blueKey = true;
                interactionObj.SetActive(false);
                break;

            // ���ǹ�(������) ��ȣ�ۿ�
            case "OpenDoor":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.GetComponent<BoxCollider>().enabled = false;

                StartCoroutine(OpenSlide(TargetObject, 0.8f, "OpenDoor"));

                TargetAudio = TargetObject.GetComponent<AudioSource>();
                TargetAudio.Play();
                break;

            // ���ǹ�(����ִ�) ��ȣ�ۿ�
            case "Door":
                TargetObject = clickManager.hit.transform;
                TargetAudio = TargetObject.GetComponent<AudioSource>();
                TargetAudio.Play();
                StartCoroutine(Shack(TargetObject));
                break;

            // �Խ��� ��ȣ�ۿ�
            case "Board":
                playerMovement.audioSource.Stop();
                cameraControl.Fixation(0f, 0f);
                TargetObject = clickManager.hit.transform;
                ZoomIn(TargetObject, 1.3f, 0f);
                break;

            // �� �繰�� ��(���������� ����) ��ȣ�ۿ�
            case "OpenObj":
                TargetObject = clickManager.hit.transform;

                // �繰�� ���� ���� ���� ���
                if (TargetObject.localEulerAngles.y < 1f)
                {
                    objectRotate.Rotation(-90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                // �繰�� ���� ���� ���� ���
                else if (TargetObject.localEulerAngles.y == 270f)
                {
                    objectRotate.Rotation(90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                break;

            // �� �繰�� ��(�������� ����) ��ȣ�ۿ�
            case "OpenObj_L":
                TargetObject = clickManager.hit.transform;

                // �繰�� ���� ���� ���� ���
                if (TargetObject.localEulerAngles.y < 1f)
                {
                    objectRotate.Rotation(90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                // �繰�� ���� ���� ���� ���
                else if (TargetObject.localEulerAngles.y == 90f)
                {
                    objectRotate.Rotation(-90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                break;

            // ������(�������� ������) ��ȣ�ۿ�
            case "SlideObj_L":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.tag = "Untagged";
                StartCoroutine(OpenSlide(TargetObject, 0.6f, "SlideObj_R"));

                audioSource = TargetObject.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[1];
                audioSource.Play();

                StartCoroutine(CheckTime());
                break;

            // ������(���������� ������) ��ȣ�ۿ�
            case "SlideObj_R":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.tag = "Untagged";
                StartCoroutine(OpenSlide(TargetObject, -0.6f, "SlideObj_L"));

                audioSource = TargetObject.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[1];
                audioSource.Play();

                StartCoroutine(CheckTime());
                break;

            // �ǵ��ư��� ��ư(Zoom-Out) ��ȣ�ۿ�
            case "ZoomOut":
                if (playerMovement.playerState == PlayerMovement.PlayerState.Limit)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Stop;
                    zoomOutButton.SetActive(false);
                    TargetObject.GetComponent<BoxCollider>().enabled = true;

                    cameraControl.camera.fieldOfView = 60f;
                    playerCamera.transform.position = cameraPosition;
                    flashLight.transform.position = flashPosition;
                }
                break;

            // ���� ���� - �̻����� �� ��ȣ�ۿ�
            case "Anomaly":
                if (playerInven.blueKey)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Limit;
                    ChooseDoor(targetTag);
                    playerMovement.MoveAnomalyHall();
                    cameraControl.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                break;

            // ���� ���� - �Ϲ� �� ��ȣ�ۿ�
            case "Normal":
                if (playerInven.blueKey)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Limit;
                    ChooseDoor(targetTag);
                    playerMovement.MoveNormalHall();
                    cameraControl.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                break;
        }

    }

    IEnumerator CheckTime()
    {
        time = 0;

        while (time < 8.0f)
        {
            time += Time.deltaTime;

            yield return null;
        }
    }

    // ������Ʈ�� Zoom-In(Ÿ�ٿ�����Ʈ, x/z�� �Ÿ�, y�� �Ÿ�)
    private void ZoomIn(Transform target, float distance, float distanceY)
    {
        cameraPosition = playerCamera.transform.position; // ī�޶� position ����
        flashPosition = flashLight.transform.position; // ������ position ����

        playerMovement.playerState = PlayerMovement.PlayerState.Limit;
        zoomOutButton.SetActive(true); // ��ư ����
        target.GetComponent<BoxCollider>().enabled = false;

        // ������Ʈ�� rotation�� ���� ī�޶��� ��ġ
        if (target.eulerAngles.y == 270)
            playerCamera.transform.position = new Vector3(target.position.x - distance, target.position.y + distanceY, target.position.z);
        else if (target.eulerAngles.y == 0)
            playerCamera.transform.position = new Vector3(target.position.x, target.position.y + distanceY, target.position.z + distance);
        else if (target.eulerAngles.y == 90)
            playerCamera.transform.position = new Vector3(target.position.x + distance, target.position.y + distanceY, target.position.z);
        else if (target.eulerAngles.y == 180)
            playerCamera.transform.position = new Vector3(target.position.x, target.position.y + distanceY, target.position.z - distance);

        // ī�޶� ��ġ�� ������ ��ġ �̵�
        flashLight.transform.position = playerCamera.transform.position;
    }

    // ��乮 ����
    IEnumerator Shack(Transform targetObject)
    {
        Vector3 originalPosition = targetObject.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeAmount;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        targetObject.localPosition = originalPosition;
    }

    // �� ��/��� ����
    IEnumerator OpenSlide(Transform targetObject, float distance, string stateTag)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(targetObject.localPosition.x + distance, targetObject.localPosition.y, targetObject.localPosition.z);
        
        float elapsedTime = 0.0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 0.3f;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            yield return null;
        }

        targetObject.gameObject.tag = stateTag;
    }

    // �� ����(�̻�����, �Ϲ� - ���� ����)
    public void ChooseDoor(string targetTag)
    {
        flashLight.GetComponent<AudioSource>().Play();
        flashLight.GetComponent<Light>().enabled = false;

        playerMovement.transform.position = new Vector3(21.5f, 1f, 22f);

        GameManager.instance.CompareAns(targetTag);
        GameManager.instance.GetStageState();

    }
}
