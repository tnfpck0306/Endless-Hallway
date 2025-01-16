using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerMovement;

/// <summary>
/// UI 관리 스크립트
/// </summary>
public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject menuPanel;
    public GameObject settingPanel;

    [SerializeField] private GameObject[] selectMenuPanel;
    [SerializeField] private ClickManager clickManager;
    [SerializeField] private Toggle toggleButton;
    [SerializeField] private AudioClip audioClip;

    private CursorLockMode cursorLockMode;
    public bool anomalyCheck = false; // 메뉴 UI 이상현상 작동여부

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

    private void Start()
    {
        if(GameManager.instance.anomalyNum != 25)
            menuPanel = selectMenuPanel[0];
        else
            menuPanel = selectMenuPanel[1];

    }

    void Update()
    {

        // ESC 키 입력시 메뉴창 띄우기 & 이상현상이 발생하지 않았을 때
        if (Input.GetKeyDown(KeyCode.Escape) && !anomalyCheck)
        {
            if(!menuPanel.activeSelf)
            {

                // 게임 상호작용 비작동
                clickManager.enabled = false;

                cursorLockMode = Cursor.lockState;
                // 커서 고정 풀기
                Cursor.lockState = CursorLockMode.Confined;

                Time.timeScale = 0;
                menuPanel.SetActive(true);

                playerMovement.playerState = PlayerState.Limit;
                playerMovement.audioSource.Stop();

                // 25번 이상현상일 경우
                if ((GameManager.instance.anomalyNum == 25))
                    ActiveAnomaly();
            }
        }
    }

    // 이상현상 활성화
    private void ActiveAnomaly()
    {
        anomalyCheck = true;

        TextMeshProUGUI menuText = menuPanel.transform.GetChild(0).Find("MenuText").GetComponent<TextMeshProUGUI>(); // menu text
        TextMeshProUGUI settingText = menuPanel.transform.GetChild(0).Find("Setting").GetComponent<TextMeshProUGUI>(); // setting text
        TextMeshProUGUI exitText = menuPanel.transform.GetChild(0).Find("Exit").GetComponent<TextMeshProUGUI>(); // exit text
        TextMeshProUGUI returnText = menuPanel.transform.GetChild(0).Find("Return").GetComponent<TextMeshProUGUI>(); // return text
        Image image = menuPanel.transform.GetChild(1).GetComponent<Image>(); // 이상현상 이미지
        AudioSource audioSource = menuPanel.transform.GetChild(1).GetComponent<AudioSource>(); // 노이즈 사운드

        StartCoroutine(ConvertText(menuText));
        StartCoroutine(ConvertText(settingText));
        StartCoroutine(ConvertText(exitText));
        StartCoroutine(ConvertText(returnText));
        StartCoroutine(AppearImage(image, audioSource));
    }

    // 메뉴의 text 변환(이상현상)
    private IEnumerator ConvertText(TextMeshProUGUI mainText)
    {
        char[] text = mainText.text.ToCharArray();

        while (menuPanel.activeSelf)
        {
            mainText.text = "";
            for(int i = 0; i < text.Length; i++)
            {
                text[i] = (char)(text[i] + 1);

                if (text[i] > 'Z')
                    text[i] = 'A';

                mainText.text += text[i];
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    // 메뉴에서 무서운 이미지 서서히 나타나기(이상현상)
    private IEnumerator AppearImage(Image image, AudioSource audioSource)
    {
        float value = 0;
        while (true)
        {
            float alpha = image.color.a + value;

            if (alpha > 0.15f)
            {
                alpha = 1f;
                audioSource.Play();
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            value = value + 0.0005f;
            
            yield return new WaitForSecondsRealtime(0.1f);

            if(alpha == 1f)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                ReturnMenuButton();

                // Ambient Light 색 변경
                RenderSettings.ambientLight = new Color(0.2f, 0f, 0f);
                break;
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

        Time.timeScale = 1;
        GameManager.instance.isFadeOut = true;
        SceneManager.LoadScene("MainMenu");
    }

    // 메뉴의 return 버튼
    public void ReturnMenuButton()
    {
        // 게임 상호작용 작동
        clickManager.enabled = true;

        // 커서를 화면 중간에 고정, 커서 숨김
        Cursor.lockState = cursorLockMode;

        playerMovement.playerState = PlayerState.Stop;

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    // 세팅의 return 버튼
    public void ReturnSettingButton()
    {
        settingPanel.SetActive(false);
    }
}
