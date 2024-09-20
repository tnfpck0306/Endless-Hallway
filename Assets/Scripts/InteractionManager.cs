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

    private Transform zoomObject;
    private Vector3 cameraPosition; // Zoom-In ���� ī�޶� ��ġ
    private Vector3 flashPosition; // Zoom-In ���� ������ ��ġ

    public void Start()
    {
        objectRotate = gameObject.GetComponent<ObjectRotate>();
    }

    public void Interaction(GameObject interactionObj)
    {
        // ���� ȹ��
        if (clickManager.rayHitString == "Key")
        {
            //clickManager.ZoomOut();
            playerInven.blueKey = true;
            interactionObj.SetActive(false);
        }

        // ���ǹ�(������) ��ȣ�ۿ�
        if (clickManager.rayHitString == "OpenDoor")
        {
            Transform door = clickManager.hit.transform;
            door.gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(OpenSlide(door, 0.8f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // ���ǹ�(����ִ�) ��ȣ�ۿ�
        if (clickManager.rayHitString == "Door")
        {
            Transform door = clickManager.hit.transform;
            AudioSource doorAudio = door.GetComponent<AudioSource>();
            doorAudio.Play();
            StartCoroutine(Shack(door));
        }

        // �Խ��� ��ȣ�ۿ�
        if (clickManager.rayHitString == "Board")
        {
            cameraControl.Fixation(0f, 0f);
            zoomObject = clickManager.hit.transform;
            ZoomIn(zoomObject, 1.3f, 0f);
        }

        // �繰�� ��ȣ�ۿ�
        if (clickManager.rayHitString == "Rack")
        {
            if (!GameManager.instance.zoomIn)
            {
                cameraControl.Fixation(20f, 0f);
                zoomObject = clickManager.hit.transform;
                ZoomIn(zoomObject, 1.3f, 1.0f);
            }
        }

        // �� �繰�� ��(���������� ����) ��ȣ�ۿ�
        if (clickManager.rayHitString == "OpenObj")
        {
            Transform openObj = clickManager.hit.transform;

            // �繰�� ���� ���� ���� ���
            if (openObj.localEulerAngles.y < 1f)
            {
                objectRotate.Rotation(-90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
            // �繰�� ���� ���� ���� ���
            else if (openObj.localEulerAngles.y == 270f)
            {
                objectRotate.Rotation(90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
        }

        // �� �繰�� ��(�������� ����) ��ȣ�ۿ�
        if (clickManager.rayHitString == "OpenObj_L")
        {
            Transform openObj = clickManager.hit.transform;

            // �繰�� ���� ���� ���� ���
            if (openObj.localEulerAngles.y < 1f)
            {
                objectRotate.Rotation(90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
            // �繰�� ���� ���� ���� ���
            else if (openObj.localEulerAngles.y == 90f)
            {
                objectRotate.Rotation(-90f, openObj);
                audioSource = openObj.parent.GetComponent<AudioSource>();
                audioSource.clip = audioManager.preloadClips[0];
                audioSource.Play();
            }
        }

        // ������(�������� ������) ��ȣ�ۿ�
        if (clickManager.rayHitString == "SlideObj_L")
        {
            Transform SlideObj = clickManager.hit.transform;
            SlideObj.gameObject.tag = "SlideObj_R";
            StartCoroutine(OpenSlide(SlideObj, 0.6f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // ������(���������� ������) ��ȣ�ۿ�
        if (clickManager.rayHitString == "SlideObj_R")
        {
            Transform SlideObj = clickManager.hit.transform;
            SlideObj.gameObject.tag = "SlideObj_L";
            StartCoroutine(OpenSlide(SlideObj, -0.6f));

            //AudioSource doorAudio = door.GetComponent<AudioSource>();
            //doorAudio.Play();
        }

        // �ǵ��ư��� ��ư(Zoom-Out) ��ȣ�ۿ�
        if (clickManager.rayHitString == "ZoomOut")
        {
            GameManager.instance.zoomIn = false;
            zoomOutButton.SetActive(false);
            zoomObject.GetComponent<BoxCollider>().enabled = true;

            playerCamera.transform.position = cameraPosition;
            flashLight.transform.position = flashPosition;
        }

    }

    // ������Ʈ�� Zoom-In(Ÿ�ٿ�����Ʈ, x/z�� �Ÿ�, y�� �Ÿ�)
    private void ZoomIn(Transform target, float distance, float distanceY)
    {
        cameraPosition = playerCamera.transform.position; // ī�޶� position ����
        flashPosition = flashLight.transform.position; // ������ position ����

        GameManager.instance.zoomIn = true;
        playerMovement.playerState = PlayerMovement.PlayerState.Stop; // �÷��̾� ���� ����
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
