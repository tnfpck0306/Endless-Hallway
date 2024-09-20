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

    private Transform zoomObject;
    private Vector3 cameraPosition; // Zoom-In 이전 카메라 위치
    private Vector3 flashPosition; // Zoom-In 이전 손전등 위치

    public void Start()
    {
        objectRotate = gameObject.GetComponent<ObjectRotate>();
    }

    public void Interaction(GameObject interactionObj)
    {
        // 열쇠 획득
        if (clickManager.rayHitString == "Key")
        {
            //clickManager.ZoomOut();
            playerInven.blueKey = true;
            interactionObj.SetActive(false);
        }

        // 교실문(열리는) 상호작용
        if (clickManager.rayHitString == "OpenDoor")
        {
            Transform door = clickManager.hit.transform;
            door.gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(OpenSlide(door, 0.8f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // 교실문(잠겨있는) 상호작용
        if (clickManager.rayHitString == "Door")
        {
            Transform door = clickManager.hit.transform;
            AudioSource doorAudio = door.GetComponent<AudioSource>();
            doorAudio.Play();
            StartCoroutine(Shack(door));
        }

        // 게시판 상호작용
        if (clickManager.rayHitString == "Board")
        {
            cameraControl.Fixation(0f, 0f);
            zoomObject = clickManager.hit.transform;
            ZoomIn(zoomObject, 1.3f, 0f);
        }

        // 사물함 상호작용
        if (clickManager.rayHitString == "Rack")
        {
            if (!GameManager.instance.zoomIn)
            {
                cameraControl.Fixation(20f, 0f);
                zoomObject = clickManager.hit.transform;
                ZoomIn(zoomObject, 1.3f, 1.0f);
            }
        }

        // 각 사물함 문(오른쪽으로 열림) 상호작용
        if (clickManager.rayHitString == "OpenObj")
        {
            Transform openObj = clickManager.hit.transform;

            // 사물함 문이 닫혀 있을 경우
            if (openObj.localEulerAngles.y < 1f)
            {
                objectRotate.Rotation(-90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
            // 사물함 문이 열려 있을 경우
            else if (openObj.localEulerAngles.y == 270f)
            {
                objectRotate.Rotation(90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
        }

        // 각 사물함 문(왼쪽으로 열림) 상호작용
        if (clickManager.rayHitString == "OpenObj_L")
        {
            Transform openObj = clickManager.hit.transform;

            // 사물함 문이 닫혀 있을 경우
            if (openObj.localEulerAngles.y < 1f)
            {
                objectRotate.Rotation(90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
            // 사물함 문이 열려 있을 경우
            else if (openObj.localEulerAngles.y == 90f)
            {
                objectRotate.Rotation(-90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
        }

        // 서랍장(왼쪽으로 열리는) 상호작용
        if (clickManager.rayHitString == "SlideObj_L")
        {
            Transform SlideObj = clickManager.hit.transform;
            SlideObj.gameObject.tag = "SlideObj_R";
            StartCoroutine(OpenSlide(SlideObj, 0.6f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // 서랍장(오른쪽으로 열리는) 상호작용
        if (clickManager.rayHitString == "SlideObj_R")
        {
            Transform SlideObj = clickManager.hit.transform;
            SlideObj.gameObject.tag = "SlideObj_L";
            StartCoroutine(OpenSlide(SlideObj, -0.6f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // 되돌아가기 버튼(Zoom-Out) 상호작용
        if (clickManager.rayHitString == "ZoomOut")
        {
            GameManager.instance.zoomIn = false;
            zoomOutButton.SetActive(false);
            zoomObject.GetComponent<BoxCollider>().enabled = true;

            playerCamera.transform.position = cameraPosition;
            flashLight.transform.position = flashPosition;
        }

    }

    // 오브젝트에 Zoom-In(타겟오브젝트, x/z축 거리, y축 거리)
    private void ZoomIn(Transform target, float distance, float distanceY)
    {
        cameraPosition = playerCamera.transform.position; // 카메라 position 저장
        flashPosition = flashLight.transform.position; // 손전등 position 저장

        GameManager.instance.zoomIn = true;
        playerMovement.playerState = PlayerMovement.PlayerState.Stop; // 플레이어 상태 정지
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
            elapsedTime += Time.deltaTime * 0.6f;
            targetObject.localPosition = Vector3.Lerp(targetObject.localPosition, targetPosition, elapsedTime);

            yield return null;
        }
    }
}
