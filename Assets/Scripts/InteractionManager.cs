using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerMovement;

/// <summary>
/// �÷��̾�� ��ȣ�ۿ� �ϴ� ������Ʈ ���� ��ũ��Ʈ
/// </summary>

public class InteractionManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject flashLight; // ������
    public GameObject Reticle; // UI Ŀ��
    public Light noticeLight; // ������ ��

    private Camera cameraA; // ���� ī�޶�
    public Camera cameraB; // ������ ī�޶�

    public Cameracontrol cameraControl; // ī�޶� ������ ����
    private ObjectRotate objectRotate;
    public ClickManager clickManager;
    public AnomalyManager anomalyManager;
    public PlayerMovement playerMovement;
    public PlayerInven playerInven;
    public AudioManager audioManager;
    private AudioSource audioSource;

    [SerializeField] private float shakeTime = 0.6f;
    [SerializeField] private float shakeSpeed = 1.0f;
    [SerializeField] private float shakeAmount = 0.5f;

    private Transform TargetObject;
    private Vector3 cameraPosition; // Zoom-In ���� ī�޶� ��ġ
    private Vector3 flashPosition; // Zoom-In ���� ������ ��ġ

    private float time; // �ð� ����

    public void Start()
    {
        cameraA = playerCamera.GetComponent<Camera>();
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

                StartCoroutine(OpenSlide(TargetObject, 0.8f, "Door"));

                audioSource = TargetObject.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[2];
                audioSource.Play();

                break;

            // ���ǹ�(����ִ�) ��ȣ�ۿ�
            case "Door":
                TargetObject = clickManager.hit.transform;
                audioSource = TargetObject.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[3];
                audioSource.Play();
                StartCoroutine(Shack(TargetObject));
                break;

            // �Խ��� ��ȣ�ۿ�
            case "Board":
                SwitchCamera();
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

            case "HideDoll":
                anomalyManager.FindeDoll(GameManager.instance.anomalyNum);
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

            // Ŭ���� �� ��ȣ�ۿ�
            case "Clear":
                // Ŀ�� ���� Ǯ��
                Cursor.lockState = CursorLockMode.Confined;
                GameManager.instance.isFadeOut = true;
                SceneManager.LoadScene("MainMenu");
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

    // ī�޶� ȭ�� ����
    public void SwitchCamera()
    {
        if (cameraA != null && cameraB != null)
        {
            // A ī�޶� ��Ȱ��ȭ, B ī�޶� Ȱ��ȭ
            cameraA.enabled = !cameraA.enabled;
            cameraB.enabled = !cameraB.enabled;

            Light mainLight = flashLight.GetComponent<Light>();
            mainLight.enabled = !mainLight.enabled;
            noticeLight.enabled = !noticeLight.enabled;

            // �÷��̾� ������ ����
            if (playerMovement.playerState != PlayerState.Limit)
            {
                playerMovement.audioSource.Stop();
                playerMovement.playerState = PlayerState.Limit;
            }
            else
                playerMovement.playerState = PlayerState.Stop;

            // Ŀ�� ���� Ǯ��
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Reticle.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Reticle.SetActive(true);
            }
        }
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

    // �� ����(�̻������ �Ϲ� �� ���� ����)
    public void ChooseDoor(string targetTag)
    {
        flashLight.GetComponent<AudioSource>().Play();
        flashLight.GetComponent<Light>().enabled = false;

        playerMovement.transform.position = new Vector3(21.5f, 1f, 22f);

        GameManager.instance.CompareAns(targetTag);
        GameManager.instance.GetStageState();

    }
}
