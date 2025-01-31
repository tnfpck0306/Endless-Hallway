using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 17번 이상현상, 손전등 고장 및 시야 밝기 트리거
/// </summary>
public class LockerMonsterTrigger : MonoBehaviour
{
    public DisappearOnLight01 trigger;
    public GameObject spotLight;
    public AudioClip offSound;
    public Animator monsterKeyAnim;
    public bool check = false; // 이상현상 활성화 여부

    private Light flashLight;
    private AudioSource audioSource;
    private float animtime;

    private void Start()
    {
        flashLight = spotLight.GetComponent<Light>();
        audioSource = spotLight.GetComponent<AudioSource>();
    }

    void Update()
    {
        animtime += Time.deltaTime;

        if(trigger.anomalyActive && !check)
        {
            check = true;
            flashLight.enabled = false;
            audioSource.clip = offSound;
            audioSource.Play();

            StartCoroutine(nightVision());
        }

        if(animtime > 1f)
        {
            monsterKeyAnim.speed = 0.0f;
        }
    }

    IEnumerator LightOff()
    {
        yield return null;
    }

    IEnumerator nightVision()
    {
        float colorValue = RenderSettings.ambientLight.r;

        yield return null;

        while (true)
        {
            RenderSettings.ambientLight = new Color(colorValue, colorValue, colorValue);
            colorValue += 0.005f;

            yield return new WaitForSeconds(0.4f);
            
            if(colorValue >= 0.115f)
            {
                break;
            }
        }
    }
}
