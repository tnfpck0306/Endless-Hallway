using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerMovement;

/// <summary>
/// UI ���� ��ũ��Ʈ
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
    public bool anomalyCheck = false; // �޴� UI �̻����� �۵�����

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

        // ESC Ű �Է½� �޴�â ���� & �̻������� �߻����� �ʾ��� ��
        if (Input.GetKeyDown(KeyCode.Escape) && !anomalyCheck)
        {
            if(!menuPanel.activeSelf)
            {

                // ���� ��ȣ�ۿ� ���۵�
                clickManager.enabled = false;

                cursorLockMode = Cursor.lockState;
                // Ŀ�� ���� Ǯ��
                Cursor.lockState = CursorLockMode.Confined;

                Time.timeScale = 0;
                menuPanel.SetActive(true);

                playerMovement.playerState = PlayerState.Limit;
                playerMovement.audioSource.Stop();

                // 25�� �̻������� ���
                if ((GameManager.instance.anomalyNum == 25))
                    ActiveAnomaly();
            }
        }
    }

    // �̻����� Ȱ��ȭ
    private void ActiveAnomaly()
    {
        anomalyCheck = true;

        TextMeshProUGUI menuText = menuPanel.transform.GetChild(0).Find("MenuText").GetComponent<TextMeshProUGUI>(); // menu text
        TextMeshProUGUI settingText = menuPanel.transform.GetChild(0).Find("Setting").GetComponent<TextMeshProUGUI>(); // setting text
        TextMeshProUGUI exitText = menuPanel.transform.GetChild(0).Find("Exit").GetComponent<TextMeshProUGUI>(); // exit text
        TextMeshProUGUI returnText = menuPanel.transform.GetChild(0).Find("Return").GetComponent<TextMeshProUGUI>(); // return text
        Image image = menuPanel.transform.GetChild(1).GetComponent<Image>(); // �̻����� �̹���
        AudioSource audioSource = menuPanel.transform.GetChild(1).GetComponent<AudioSource>(); // ������ ����

        StartCoroutine(ConvertText(menuText));
        StartCoroutine(ConvertText(settingText));
        StartCoroutine(ConvertText(exitText));
        StartCoroutine(ConvertText(returnText));
        StartCoroutine(AppearImage(image, audioSource));
    }

    // �޴��� text ��ȯ(�̻�����)
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

    // �޴����� ������ �̹��� ������ ��Ÿ����(�̻�����)
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

                // Ambient Light �� ����
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

    // �޴��� setting ��ư
    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }

    // �޴��� exit ��ư
    public void ExitButton()
    {

        Time.timeScale = 1;
        GameManager.instance.isFadeOut = true;
        SceneManager.LoadScene("MainMenu");
    }

    // �޴��� return ��ư
    public void ReturnMenuButton()
    {
        // ���� ��ȣ�ۿ� �۵�
        clickManager.enabled = true;

        // Ŀ���� ȭ�� �߰��� ����, Ŀ�� ����
        Cursor.lockState = cursorLockMode;

        playerMovement.playerState = PlayerState.Stop;

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    // ������ return ��ư
    public void ReturnSettingButton()
    {
        settingPanel.SetActive(false);
    }
}
