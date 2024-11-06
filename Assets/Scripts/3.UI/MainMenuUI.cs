using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingPanel;

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
