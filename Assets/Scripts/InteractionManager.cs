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

    public Cameracontrol cameraControl; // 카메라 움직임 참조
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
    private Vector3 cameraPosition; // Zoom-In 이전 카메라 위치
    private Vector3 flashPosition; // Zoom-In 이전 손전등 위치

    public void Start()
    {
        objectRotate = gameObject.GetComponent<ObjectRotate>();
    }

    public void Interaction(GameObject interactionObj)
    {
        switch (clickManager.rayHitString)
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

                StartCoroutine(OpenSlide(TargetObject, 0.8f));

                TargetAudio = TargetObject.GetComponent<AudioSource>();
                TargetAudio.Play();
                break;

            // 교실문(잠겨있는) 상호작용
            case "Door":
                TargetObject = clickManager.hit.transform;
                TargetAudio = TargetObject.GetComponent<AudioSource>();
                TargetAudio.Play();
                StartCoroutine(Shack(TargetObject));
                break;

            // 게시판 상호작용
            case "Board":
                playerMovement.audioSource.Stop();
                cameraControl.Fixation(0f, 0f);
                TargetObject = clickManager.hit.transform;
                ZoomIn(TargetObject, 1.3f, 0f);
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
                TargetObject.gameObject.tag = "SlideObj_R";
                StartCoroutine(OpenSlide(TargetObject, 0.6f));

                //audioSource = TargetObject.GetComponent<AudioSource>();
                //audioSource.Play();
                break;

            // 서랍장(오른쪽으로 열리는) 상호작용
            case "SlideObj_R":
                TargetObject = clickManager.hit.transform;
                TargetObject.gameObject.tag = "SlideObj_L";
                StartCoroutine(OpenSlide(TargetObject, -0.6f));

                //audioSource = TargetObject.GetComponent<AudioSource>();
                //audioSource.Play();
                break;

            // 되돌아가기 버튼(Zoom-Out) 상호작용
            case "ZoomOut":
                playerMovement.playerState = PlayerMovement.PlayerState.Stop;
                zoomOutButton.SetActive(false);
                TargetObject.GetComponent<BoxCollider>().enabled = true;

                playerCamera.transform.position = cameraPosition;
                flashLight.transform.position = flashPosition;
                break;
        }

    }

    // 오브젝트에 Zoom-In(타겟오브젝트, x/z축 거리, y축 거리)
    private void ZoomIn(Transform target, float distance, float distanceY)
    {
        cameraPosition = playerCamera.transform.position; // 카메라 position 저장
        flashPosition = flashLight.transform.position; // 손전등 position 저장

        playerMovement.playerState = PlayerMovement.PlayerState.Limit;
        zoomOutButton.SetActive(true); // 버튼 생성
        target.GetComponent<BoxCollider>().enabled = false;

        // 오브젝트의 rotation에 따른 카메라의 위치
        if (target.eulerAngles.y == 270)
            playerCamera.transform.position = new Vector3(target.position.x - distance, target.position.y + distanceY, target.position.z);
        else if (target.eulerAngles.y == 0)
            playerCamera.transform.position = new Vector3(target.position.x, target.position.y + distanceY, target.position.z + distance);
        else if (target.eulerAngles.y == 90)
            playerCamera.transform.position = new Vector3(target.position.x + distance, target.position.y + distanceY, target.position.z);
        else if (target.eulerAngles.y == 180)
            playerCamera.transform.position = new Vector3(target.position.x, target.position.y + distanceY, target.position.z - distance);

        // 카메라 위치에 손전등 위치 이동
        flashLight.transform.position = playerCamera.transform.position;
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
    IEnumerator OpenSlide(Transform targetObject, float distance)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(targetObject.localPosition.x + distance, targetObject.localPosition.y, targetObject.localPosition.z);
        
        float elapsedTime = 0.0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 0.4f;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            yield return null;
        }
    }
}
