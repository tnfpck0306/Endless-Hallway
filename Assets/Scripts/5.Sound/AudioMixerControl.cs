using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// ����â�� �̿��� ���� ���� ��ũ��Ʈ
/// </summary>
public class AudioMixerControl : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer; // ����� �ͼ�
    [SerializeField] private Slider m_MasterSlider; // ������ ���� �����̴�
    [SerializeField] private Slider m_SFXSlider; // SFX ���� �����̴�
    [SerializeField] private Text MasterVolumeText; // ������ �ҷ� �ؽ�Ʈ
    [SerializeField] private Text SFXVolumeText; // SFX �ҷ� �ؽ�Ʈ

    private void Awake()
    {
        m_MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_SFXSlider.onValueChanged.AddListener(SetSFXVolume);

        m_MasterSlider.value = 1;
        m_SFXSlider.value = 1;
    }

    // ������ ���� �����̴� �� �����
    public void SetMasterVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        MasterVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    // SFX ���� �����̴� �� �����
    public void SetSFXVolume(float volume)
    {
        int volumeText = (int)(volume * 100);
        SFXVolumeText.text = volumeText.ToString();
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
