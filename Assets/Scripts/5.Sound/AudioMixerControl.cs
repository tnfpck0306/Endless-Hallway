using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// 설정창을 이용한 사운드 관리 스크립트
/// </summary>
public class AudioMixerControl : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer; // 오디오 믹서
    [SerializeField] private Slider m_MasterSlider; // 마스터 볼륨 슬라이더
    [SerializeField] private Slider m_BGMSlider; // BGM 볼륨 슬라이더
    [SerializeField] private Slider m_SFXSlider; // SFX 볼륨 슬라이더
    [SerializeField] private Text MasterVolumeText; // 마스터 불륨 텍스트
    [SerializeField] private Text BGMVolumeText; // BGM 불륨 텍스트
    [SerializeField] private Text SFXVolumeText; // SFX 불륨 텍스트

    private void Awake()
    {
        m_MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        m_BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    private void Start()
    {

        if (PlayerPrefs.HasKey("MasterVol"))
            m_MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        else
            m_MasterSlider.value = 1;

        if (PlayerPrefs.HasKey("SFXVol"))
            m_SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        else
            m_SFXSlider.value = 1;

        if (PlayerPrefs.HasKey("BGMVol"))
            m_BGMSlider.value = PlayerPrefs.GetFloat("BGMVol");
        else
            m_BGMSlider.value = 1;
    }

    // 마스터 볼륨 슬라이더 값 변경시
    public void SetMasterVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        MasterVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVol", volume);
    }

    // SFX 볼륨 슬라이더 값 변경시
    public void SetSFXVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        SFXVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    // BGM 볼륨 슬라이더 값 변경시
    public void SetBGMVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        BGMVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVol", volume);
    }
}
