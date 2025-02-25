using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// 설정창을 이용한 사운드 관리 스크립트
/// </summary>
public class AudioMixerControl_InGame : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer; // 오디오 믹서
    [SerializeField] private Slider m_MasterSlider; // 마스터 볼륨 슬라이더
    [SerializeField] private Slider m_SFXSlider; // SFX 볼륨 슬라이더
    [SerializeField] private Text MasterVolumeText; // 마스터 불륨 텍스트
    [SerializeField] private Text SFXVolumeText; // SFX 불륨 텍스트

    private void Awake()
    {
        float MasterVolume;
        float SFXVolume;

        m_MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_SFXSlider.onValueChanged.AddListener(SetSFXVolume);

        m_AudioMixer.GetFloat("Master", out MasterVolume);
        m_AudioMixer.GetFloat("SFX", out SFXVolume);

        m_MasterSlider.value = Mathf.Pow(10, MasterVolume / 20);
        m_SFXSlider.value = Mathf.Pow(10, SFXVolume / 20);
    }

    // 마스터 볼륨 슬라이더 값 변경시
    public void SetMasterVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        MasterVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    // SFX 볼륨 슬라이더 값 변경시
    public void SetSFXVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        SFXVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
