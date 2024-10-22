using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    public GameObject panel; // 패널 오브젝트
    private Action onCompleteCallback;

    private void Awake()
    {
        // 페이드 아웃 상태였으면 이후 페이드 인
        if (GameManager.instance.isFadeOut)
        {
            FadeIn();
            GameManager.instance.isFadeOut = false;
        }
    }

    // 페이드 인
    public void FadeIn()
    {
        panel.SetActive(true);
        StartCoroutine(CoFadeIn());
    }

    // 페이드 아웃
    public void FadeOut()
    {
        panel.SetActive(true) ;
        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadeTime = 1f; // 총 소요 시간

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
        float elapsedTime = 0f; // 누적 경과 시간
        float fadeTime = 1f; // 총 소요 시간

        Image panelImage = panel.GetComponent<Image>();

        while (elapsedTime <= fadeTime)
        {
            panelImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, elapsedTime / fadeTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke(); // Fade out 이후 해야 하는 액션
        yield break;
    }

    public void RegisterCallback(Action callback) // 다른 스크립트에서 콜백 액션을 등록하기 위해 사용
    {
        onCompleteCallback = callback;
    }
}
