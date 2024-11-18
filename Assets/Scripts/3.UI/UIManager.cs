using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI 관리 스크립트
/// </summary>
public class UIManager : MonoBehaviour
{
    public Text stageStateText;
    public Text stageText;
    public Text playerStateText;

    public PlayerMovement playerMovement;
    public GameObject menuPanel;
    public GameObject settingPanel;

    [SerializeField] private Toggle toggleButton;

    private void Awake()
    {
        toggleButton.onValueChanged.AddListener(ToggleFullScreen);

        if(Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            toggleButton.isOn = true;
        }
        else
        {
            toggleButton.isOn = false;
        }
    }

    void Update()
    {
        playerStateText.text = $"PlayerState : {playerMovement.playerState}";
        stageStateText.text = $"Stage State : {GameManager.instance.anomalyNum}{GameManager.instance.stageState}";
        stageText.text = $"Stage : {GameManager.instance.stage}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menuPanel.activeSelf)
            {
                // 오디오 리스너 음소거
                AudioListener.pause = true;

                // 커서 고정 풀기
                Cursor.lockState = CursorLockMode.Confined;

                Time.timeScale = 0;
                menuPanel.SetActive(true);
            }
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

    // 메뉴의 setting 버튼
    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    // 메뉴의 exit 버튼
    public void ExitButton()
    {
        // 오디오 리스너 음소거 해제
        AudioListener.pause = false;

        Time.timeScale = 1;
        GameManager.instance.isFadeOut = true;
        SceneManager.LoadScene("MainMenu");
    }

    // 메뉴의 return 버튼
    public void ReturnMenuButton()
    {
        // 오디오 리스너 음소거 해제
        AudioListener.pause = false;

        // 커서를 화면 중간에 고정, 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    // 세팅의 return 버튼
    public void ReturnSettingButton()
    {
        settingPanel.SetActive(false);
    }
}
