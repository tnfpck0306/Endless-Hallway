using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerMovement;

/// <summary>
/// 플레이어와 상호작용 하는 오브젝트 관리 스크립트
/// </summary>

public class InteractionManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject flashLight; // 손전등
    public GameObject Reticle; // UI 커서
    public Light noticeLight; // 공지문 빛

    private Camera cameraA; // 메인 카메라
    public Camera cameraB; // 공지문 카메라

    public Cameracontrol cameraControl; // 카메라 움직임 참조
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
    private Vector3 cameraPosition; // Zoom-In 이전 카메라 위치
    private Vector3 flashPosition; // Zoom-In 이전 손전등 위치

    private float time; // 시간 측정

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
            // 열쇠 획득
            case "Key":
                playerInven.blueKey = true;
                interactionObj.SetActive(false);
                break;

            // 교실문(열리는) 상호작용
            case "OpenDoor":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.GetComponent<BoxCollider>().enabled = false;

                StartCoroutine(OpenSlide(TargetObject, 0.8f, "Door"));

                audioSource = TargetObject.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[2];
                audioSource.Play();

                break;

            // 교실문(잠겨있는) 상호작용
            case "Door":
                TargetObject = clickManager.hit.transform;
                audioSource = TargetObject.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[3];
                audioSource.Play();
                StartCoroutine(Shack(TargetObject));
                break;

            // 게시판 상호작용
            case "Board":
                SwitchCamera();
                break;

            // 각 사물함 문(오른쪽으로 열림) 상호작용
            case "OpenObj":
                TargetObject = clickManager.hit.transform;

                // 사물함 문이 닫혀 있을 경우
                if (TargetObject.localEulerAngles.y < 1f)
                {
                    objectRotate.Rotation(-90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                // 사물함 문이 열려 있을 경우
                else if (TargetObject.localEulerAngles.y == 270f)
                {
                    objectRotate.Rotation(90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                break;

            // 각 사물함 문(왼쪽으로 열림) 상호작용
            case "OpenObj_L":
                TargetObject = clickManager.hit.transform;

                // 사물함 문이 닫혀 있을 경우
                if (TargetObject.localEulerAngles.y < 1f)
                {
                    objectRotate.Rotation(90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                // 사물함 문이 열려 있을 경우
                else if (TargetObject.localEulerAngles.y == 90f)
                {
                    objectRotate.Rotation(-90f, TargetObject);
                    audioSource = TargetObject.parent.GetComponent<AudioSource>();
                    audioSource.clip = audioManager.preloadClips[0];
                    audioSource.Play();
                }
                break;

            // 서랍장(왼쪽으로 열리는) 상호작용
            case "SlideObj_L":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.tag = "Untagged";
                StartCoroutine(OpenSlide(TargetObject, 0.6f, "SlideObj_R"));

                audioSource = TargetObject.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[1];
                audioSource.Play();

                StartCoroutine(CheckTime());
                break;

            // 서랍장(오른쪽으로 열리는) 상호작용
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

            // 정답 선택 - 이상현상 문 상호작용
            case "Anomaly":
                if (playerInven.blueKey)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Limit;
                    ChooseDoor(targetTag);
                    playerMovement.MoveAnomalyHall();
                    cameraControl.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                break;

            // 정답 선택 - 일반 문 상호작용
            case "Normal":
                if (playerInven.blueKey)
                {
                    playerMovement.playerState = PlayerMovement.PlayerState.Limit;
                    ChooseDoor(targetTag);
                    playerMovement.MoveNormalHall();
                    cameraControl.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                break;

            // 클리어 문 상호작용
            case "Clear":
                // 커서 고정 풀기
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

    // 카메라 화면 변경
    public void SwitchCamera()
    {
        if (cameraA != null && cameraB != null)
        {
            // A 카메라 비활성화, B 카메라 활성화
            cameraA.enabled = !cameraA.enabled;
            cameraB.enabled = !cameraB.enabled;

            Light mainLight = flashLight.GetComponent<Light>();
            mainLight.enabled = !mainLight.enabled;
            noticeLight.enabled = !noticeLight.enabled;

            // 플레이어 움직임 제한
            if (playerMovement.playerState != PlayerState.Limit)
            {
                playerMovement.audioSource.Stop();
                playerMovement.playerState = PlayerState.Limit;
            }
            else
                playerMovement.playerState = PlayerState.Stop;

            // 커서 고정 풀기
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

    // 잠긴문 흔들기
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

    // 문 좌/우로 열기
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

    // 문 선택(이상현상과 일반 중 정답 선택)
    public void ChooseDoor(string targetTag)
    {
        flashLight.GetComponent<AudioSource>().Play();
        flashLight.GetComponent<Light>().enabled = false;

        playerMovement.transform.position = new Vector3(21.5f, 1f, 22f);

        GameManager.instance.CompareAns(targetTag);
        GameManager.instance.GetStageState();

    }
}
