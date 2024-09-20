using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임의 스테이지, 상태여부, 이상현상 종류를 관리하는 게임 매니저
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

    public enum StageState
    {
        Normal,
        Anomaly
    }

    public StageState stageState;
    public Anomaly anomaly;

    public bool zoomIn;
    public int randStage; // 이상현상 확률
    public int randStage_max;
    public int stage;

    public List<int> anomalyData;
    public int anomalyNum; // 이상현상 종류

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
        zoomIn = false;
        stageState = StageState.Normal;
        //randStage = 4;
        randStage_max = 5;
        stage = 0;

        anomalyData = new List<int>(anomaly.anomalyList);
        anomalyNum = 0;
    }

    public void GetStageState()
    {
        randStage = Random.Range(0, randStage_max);
        // 첫 스테이지는 정상적인 상태
        if (stage == 0)
            stageState = StageState.Normal;

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


    // 게임오버 처리
    public void EndGame()
    {
        
    }
}
