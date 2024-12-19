using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackBoardText : MonoBehaviour
{
    public TextMeshPro displayText; // TextMeshProUGUI 컴포넌트
    [SerializeField] private string fullText = "watching you"; // 출력할 텍스트
    [SerializeField] private float delay = 0.2f; // 한 글자당 출력 지연 시간

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
        while (true) // 무한 반복
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                displayText.text += fullText[i]; // 글자 추가
                yield return new WaitForSeconds(delay);
            }
            displayText.text += " "; // 단어 사이 공백 추가

            if (displayText.text.Length > 100)
            {
                break;
            }
        }
    }
}
