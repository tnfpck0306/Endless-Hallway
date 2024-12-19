using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackBoardText : MonoBehaviour
{
    public TextMeshPro displayText; // TextMeshProUGUI ������Ʈ
    [SerializeField] private string fullText = "watching you"; // ����� �ؽ�Ʈ
    [SerializeField] private float delay = 0.2f; // �� ���ڴ� ��� ���� �ð�

    public DetectLight detectLight;
    private bool activeCheck = false;

    private void Update()
    {
        if (detectLight.isTriggerActive && !activeCheck)
        {
            StartCoroutine(TypeTextContinuously());
            activeCheck = true;
        }
    }

    private IEnumerator TypeTextContinuously()
    {
        while (true) // ���� �ݺ�
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                displayText.text += fullText[i]; // ���� �߰�
                yield return new WaitForSeconds(delay);
            }
            displayText.text += " "; // �ܾ� ���� ���� �߰�

            if (displayText.text.Length > 100)
            {
                break;
            }
        }
    }
}
