using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �޴� ��ư �� ���α׷� ���÷��� ���� ��ũ��Ʈ
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Toggle toggleButton;
    public GameObject settingPanel;

    private void Awake()
    {
        toggleButton.onValueChanged.AddListener(ToggleFullScreen);

        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            toggleButton.isOn = true;
        }
        else
        {
            toggleButton.isOn = false;
        }
    }

    public void ToggleFullScreen(bool isOn)
    {
        if (isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Endless Hallway01");
    }

    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    public void ReturnButton()
    {
        settingPanel.SetActive(false);
    }
}
