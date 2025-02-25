using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 게임의 스테이지, 상태여부, 게임오버를 관리하는 게임 매니저
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance;

    // 스테이지 상태(일반, 이상현상)
    public enum StageState
    {
        Normal,
        Anomaly
    }

    public StageState stageState;
    public Anomaly anomaly;

    [SerializeField] private int endingStage;
    private int randStage; // 이상현상 확률
    private int randStage_max;
    public int stage; // 게임 스테이지
    public bool condition; // 게임 조건 충족 여부 확인

    public List<int> anomalyData;
    public int anomalyNum; // 이상현상 종류

    public FadeControl fadeControl; // 페이드 인/아웃
    public bool isFadeOut = true;
    private Camera mainCamera;
    private Camera animCamera;

    [SerializeField] private TestMode testMode;
    [SerializeField] private bool testOn;
    private int testNum = 0;

    private void Awake()
    {

        if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        stageState = StageState.Normal;
        randStage_max = 5;
        stage = 0;
        condition = true;

        anomalyData = new List<int>(anomaly.anomalyList);
        anomalyNum = 0;

        testOn = testMode.testModeON;

    }

    // 스테이지 상태 설정
    public void GetStageState()
    {
        randStage = Random.Range(0, randStage_max);
        // 첫 스테이지는 정상적인 상태
        if (stage == 0)
            stageState = StageState.Normal;

        // 테스트 버전에서 이상현상 1번부터 차례대로 확인
        else if (testOn)
        {
            stageState = StageState.Anomaly;
            anomalyNum = ++testNum;
        }

        else // 그 외 스테이지(변동확률 : 이상현상 75% / 정상 25% <이상현상 발생 시 정상 확률 증가> )
        {

            if (randStage > 3) // 정상적일 때(이상현상이 없을 때)
            {
                stageState = StageState.Normal;
                anomalyNum = 0;
                randStage_max = 5; // 확률 복구
            }

            else // 이상현상이 있을 때
            {
                stageState = StageState.Anomaly;
                anomalyNum = anomalyData[Random.Range(0, anomalyData.Count)];
                // 다음 스테이지 정상일 확률 증가(이상현상 확률 감소)
                if (randStage_max < 8)
                {
                    randStage_max++;
                }
            }
        }
    }

    // 정답비교
    public void CompareAns(string choice)
    {
        // 테스트 버전에서 확인
        if (testOn)
        {
            // 클리어한 이상현상 저장
            string anomalyKey = anomalyNum.ToString();
            PlayerPrefs.SetInt(anomalyKey, 1);
        }

        // 정답인 경우
        else if (choice.Equals(stageState.ToString()) && condition)
        {
            stage++; // 스테이지 증가
            anomalyData.Remove(GameManager.instance.anomalyNum);

            // 클리어한 이상현상 저장
            string anomalyKey = anomalyNum.ToString();
            PlayerPrefs.SetInt(anomalyKey, 1);
        }

        // 오답인 경우
        else
        {
            // 스테이지 초기화(0스테이지 부터)
            condition = true;
            stage = 0;
            randStage_max = 5;
            anomalyData = new List<int>(anomaly.anomalyList);
            anomalyNum = 0;
        }
    }


    // 게임오버 처리
    public void EndGame()
    {
        fadeControl = GameObject.Find("Canvas").GetComponent<FadeControl>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        animCamera = GameObject.Find("AnimCamera").GetComponent<Camera>();

        // 스테이지 초기화
        condition = true;
        stage = 0;
        randStage_max = 5;
        anomalyData = new List<int>(anomaly.anomalyList);
        anomalyNum = 0;

        // 테스트 버전에서 게임오버 이후 stage의 이상현상은 동일
        if (testOn)
        {
            stageState = StageState.Anomaly;
            anomalyNum = testNum;
        }
        else
        {
            GetStageState();
        }

        StartCoroutine(GameOverAnim());
    }

    IEnumerator GameOverAnim()
    {
        mainCamera.enabled = !mainCamera.enabled;
        animCamera.enabled = !animCamera.enabled;

        yield return new WaitForSeconds(3f);

        // 페이드 아웃
        fadeControl.FadeOut();
        fadeControl.RegisterCallback(SceneReset);
    }

    // 씬 전환
    public void ChangeScean()
    {
        // 게임의 마지막 단계가 아닌 경우
        if (stage != endingStage)
        {
            // 23번 이상현상(복도탈출)인 경우
            if (anomalyNum == 23)
                SceneManager.LoadScene("Endless Hallway02"); // Hallway02 씬으로 이동

            else 
                SceneManager.LoadScene("Endless Hallway01"); // Hallway01 씬으로 이동
        }
        else // 게임의 마지막 단계인 경우
        {
            anomalyNum = 0;
            SceneManager.LoadScene("Ending Hallway"); // 엔딩 씬으로 이동
        }
    }

    // 씬 초기화
    private void SceneReset()
    {
        isFadeOut = true;
        SceneManager.LoadScene("Endless Hallway01");

    }
}
