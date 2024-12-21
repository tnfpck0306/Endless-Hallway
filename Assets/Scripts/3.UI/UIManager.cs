using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI ���� ��ũ��Ʈ
/// </summary>
public class UIManager : MonoBehaviour
{
    public Text stageStateText;
    public Text stageText;
    public Text playerStateText;

    public PlayerMovement playerMovement;
    public GameObject menuPanel;
    public GameObject settingPanel;
    private Cursor cursorState;

    [SerializeField] private ClickManager clickManager;
    [SerializeField] private Toggle toggleButton;
    private CursorLockMode cursorLockMode;

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
                // ���� ��ȣ�ۿ� ���۵�
                clickManager.enabled = false;

                // ����� ������ ���Ұ�
                AudioListener.pause = true;

                cursorLockMode = Cursor.lockState;
                // Ŀ�� ���� Ǯ��
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

    // �޴��� setting ��ư
    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    // �޴��� exit ��ư
    public void ExitButton()
    {
        // ����� ������ ���Ұ� ����
        AudioListener.pause = false;

        Time.timeScale = 1;
        GameManager.instance.isFadeOut = true;
        SceneManager.LoadScene("MainMenu");
    }

    // �޴��� return ��ư
    public void ReturnMenuButton()
    {
        // ���� ��ȣ�ۿ� �۵�
        clickManager.enabled = true;

        // ����� ������ ���Ұ� ����
        AudioListener.pause = false;

        // Ŀ���� ȭ�� �߰��� ����, Ŀ�� ����
        Cursor.lockState = cursorLockMode;

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    // ������ return ��ư
    public void ReturnSettingButton()
    {
        settingPanel.SetActive(false);
    }
}
