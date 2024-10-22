using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    public GameObject panel; // �г� ������Ʈ
    private Action onCompleteCallback;

    private void Awake()
    {
        // ���̵� �ƿ� ���¿����� ���� ���̵� ��
        if (GameManager.instance.isFadeOut)
        {
            FadeIn();
            GameManager.instance.isFadeOut = false;
        }
    }

    // ���̵� ��
    public void FadeIn()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeIn());
    }

    // ���̵� �ƿ�
    public void FadeOut()
    {
        panel.SetActive(true) ;
        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // ���� ��� �ð�
        float fadeTime = 1f; // �� �ҿ� �ð�

        Image panelImage = panel.GetComponent<Image>();

        while (elapsedTime <= fadeTime)
        {
            panelImage.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, elapsedTime / fadeTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.SetActive(false);
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; // ���� ��� �ð�
        float fadeTime = 1f; // �� �ҿ� �ð�

        Image panelImage = panel.GetComponent<Image>();

        while (elapsedTime <= fadeTime)
        {
            panelImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, elapsedTime / fadeTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke(); // Fade out ���� �ؾ� �ϴ� �׼�
        yield break;
    }

    public void RegisterCallback(Action callback) // �ٸ� ��ũ��Ʈ���� �ݹ� �׼��� ����ϱ� ���� ���
    {
        onCompleteCallback = callback;
    }
}
