using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 8번 이상현상(피 묻은 손바닥) 작동 트리거 스크립트
/// </summary>
public class PalmTrigger : MonoBehaviour
{
    public GameObject[] RedPalm; // 피 묻은 손바닥
    public AudioSource audioSource; // 손바닥 AudioSource
    public AudioManager audioManager; // 손바닥 사운드 클립
    public Light flashLight; // 손전등

    private Vector3 targetLeft; // 왼손 목적 위치
    private Vector3 targetRight; // 오른손 목적 위치
    private Vector3 startLeft; // 왼손 초기 위치
    private Vector3 startRight; // 오른손 초기 위치
    public GameObject LeftHand; // 점프스케어 왼손
    public GameObject RightHand; // 점프스케어 오른손

    [SerializeField] private float moveDuration = 2.5f; // 작동 시간

    private bool EndCheck = false; // 트리거 종료 여부

    private void Start()
    {
        startLeft = LeftHand.transform.localPosition;
        startRight = RightHand.transform.localPosition;

        targetLeft = new Vector3(-0.28f, -0.043f, 0.4f);
        targetRight = new Vector3(0.28f, -0.043f, 0.4f);
    }

    // 트리거
    private void OnTriggerEnter(Collider other)
    {
        LeftHand.SetActive(true);
        RightHand.SetActive(true);

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Handprint());
            StartCoroutine(blink());
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // 피 묻은 손자국 찍기
    IEnumerator Handprint()
    {
        int printMiddle = RedPalm.Length / 2;
        int count = printMiddle + 1;

        int printQuarter = (count / 2) + 1;
        float interval = 0.35f;

        for (int i = 0; i < printQuarter; i++)
        {

            RedPalm[i].SetActive(true);
            audioSource.clip = audioManager.preloadClips[5];
            audioSource.Play();

            if (i != 0)
            {
                RedPalm[count - i].SetActive(true);
                RedPalm[i + printMiddle].SetActive(true);
                RedPalm[RedPalm.Length - i].SetActive(true);
                audioSource.clip = audioManager.preloadClips[6];
                audioSource.Play();
                yield return new WaitForSeconds(interval);
            }

            else
                yield return new WaitForSeconds(0.7f);

            if (interval > 0.15f)
                interval -= 0.05f;
        }

        EndCheck = true;
        yield return new WaitForSeconds(1f);

        StartCoroutine(HandMovement());
    }

    // 점프스케어 손 움직임
    IEnumerator HandMovement()
    {
        float elapsedTime = 0f;
        while(elapsedTime < moveDuration)
        {
            LeftHand.transform.localPosition = Vector3.Lerp(startLeft, targetLeft, elapsedTime / moveDuration);
            RightHand.transform.localPosition = Vector3.Lerp(startRight, targetRight, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LeftHand.transform.localPosition = targetLeft;
        RightHand.transform.localPosition = targetRight;

        flashLight.enabled = false;
        LeftHand.SetActive(false);
        RightHand.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        flashLight.enabled = true;
    }

    // 손전등 깜박임
    IEnumerator blink()
    {
        yield return new WaitForSeconds(0.7f);

        while (!EndCheck) {
            flashLight.enabled = false;

            yield return new WaitForSeconds(0.1f);

            flashLight.enabled = true;

            yield return new WaitForSeconds(0.1f);
        }

    }
}
