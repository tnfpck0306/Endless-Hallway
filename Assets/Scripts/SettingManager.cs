using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public float MainVolumeData = 1;
    public float SFXVolumeData = 1;

    public Slider MainVolumeSlider;
    public Slider SFXVolumeSlider;

    public Text MainVolumeText;
    public Text SFXVolumeText;

    public void Start()
    {
        MainVolumeSlider.value = MainVolumeData;
        SFXVolumeSlider.value = SFXVolumeData;

        int mainValue = (int)(MainVolumeData * 100);
        int SFXValue = (int)(SFXVolumeData * 100);

        MainVolumeText.text = mainValue.ToString();
        SFXVolumeText.text = SFXValue.ToString();
    }

    public void ValueChange()
    {
        MainVolumeData = MainVolumeSlider.value;
        SFXVolumeData = SFXVolumeSlider.value;

        int mainValue = (int)(MainVolumeData * 100);
        int SFXValue = (int)(SFXVolumeData * 100);

        MainVolumeText.text = mainValue.ToString();
        SFXVolumeText.text = SFXValue.ToString();
    }
}
