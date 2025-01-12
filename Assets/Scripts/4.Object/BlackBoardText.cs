using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// ĥ�� �̻����� ��ũ��Ʈ
/// </summary>
public class BlackBoardText : MonoBehaviour
{
    public TextMeshPro displayText01; // TextMeshPro ������Ʈ
    public TextMeshPro displayTextSub01;
    public TextMeshPro displayText02;
    public TextMeshPro displayTextSub02;
    public TextMeshPro displayText03;
    public TextMeshPro displayText04;
    public TextMeshPro[] wallDisplayText;

    [SerializeField] private string fullText01 = "no way out"; // ����� �ؽ�Ʈ
    [SerializeField] private string fullText02 = "no hope";
    [SerializeField] private string fullText03 = "give up";
    [SerializeField] private string fullText04 = "despair fear";
    [SerializeField] private string wallText= "die";

    [SerializeField] private float delay = 0.2f; // �� ���ڴ� ��� ���� �ð�

    public DetectLight detectLight;
    private AudioSource audioSource01;
    private AudioSource audioSource02;
    private bool activeCheck = false;

    private void Update()
    {
        if (detectLight.isTriggerActive && !activeCheck)
        {
            audioSource01 = displayText01.GetComponent<AudioSource>();
            audioSource02 = displayText02.GetComponent<AudioSource>();

            audioSource01.pitch = 1.2f;
            audioSource02.pitch = 1.2f;
            audioSource01.Play();
            audioSource02.Play();

            StartCoroutine(TypeTextContinuously(displayText01, fullText01, 200));
            StartCoroutine(TypeTextContinuously(displayTextSub01, fullText01, 200));
            StartCoroutine(TypeTextContinuously(displayText02, fullText02, 213));
            StartCoroutine(TypeTextContinuously(displayTextSub02, fullText02, 213));
            StartCoroutine(TypeTextContinuously(displayText03, fullText03, 94));
            StartCoroutine(TypeTextContinuously(displayText04, fullText04, 100));
            activeCheck = true;

            for(int i = 0; i < wallDisplayText.Length; i++)
            {
                StartCoroutine(TypeTextContinuously(wallDisplayText[i], wallText, 130));
            }
        }
    }

    private IEnumerator TypeTextContinuously(TextMeshPro displayText, string fullText, int maxTextLength)
    {
        while (true) // ���� �ݺ�
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                if (displayText.text.Length > maxTextLength)
                {
                    break;
                }


                displayText.text += fullText[i]; // ���� �߰�
                yield return new WaitForSeconds(delay);
            }

            if (displayText.text.Length > maxTextLength)
            {
                if(maxTextLength == 213)
                {
                    audioSource01.Stop();
                    audioSource02.Stop();
                }
                break;
            }

            displayText.text += " "; // �ܾ� ���� ���� �߰�
        }
    }
}
