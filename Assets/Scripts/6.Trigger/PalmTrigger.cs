using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 8�� �̻�����(�� ���� �չٴ�) �۵� Ʈ���� ��ũ��Ʈ
/// </summary>
public class PalmTrigger : MonoBehaviour
{
    public GameObject[] RedPalm; // �� ���� �չٴ�
    public AudioSource audioSource; // �չٴ� AudioSource
    public AudioManager audioManager; // �չٴ� ���� Ŭ��
    public Light flashLight; // ������

    private Vector3 targetLeft; // �޼� ���� ��ġ
    private Vector3 targetRight; // ������ ���� ��ġ
    private Vector3 startLeft; // �޼� �ʱ� ��ġ
    private Vector3 startRight; // ������ �ʱ� ��ġ
    public GameObject LeftHand; // �������ɾ� �޼�
    public GameObject RightHand; // �������ɾ� ������

    [SerializeField] private float moveDuration = 2.5f; // �۵� �ð�

    private bool EndCheck = false; // Ʈ���� ���� ����

    private void Start()
    {
        startLeft = LeftHand.transform.localPosition;
        startRight = RightHand.transform.localPosition;

        targetLeft = new Vector3(-0.28f, -0.043f, 0.4f);
        targetRight = new Vector3(0.28f, -0.043f, 0.4f);
    }

    // Ʈ����
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

    // �� ���� ���ڱ� ���
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

    // �������ɾ� �� ������
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

    // ������ ������
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
