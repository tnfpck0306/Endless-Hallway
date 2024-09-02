using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// ����(ų ��)�� ���ӿ��� ���θ� �����ϴ� ���� �Ŵ���
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

    public bool zoomIn;
    public int randStage; // �̻����� Ȯ��
    public int randStage_max;
    public int stage;

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
        randStage = 4;
        randStage_max = 5;
        stage = 0;
    }

    public void GetStageState()
    {
        randStage = Random.Range(0, randStage_max);

        if (stage == 0)
            randStage = 4;

        if (randStage > 3)
        {
            stageState = StageState.Normal;
            randStage_max = 5;
        }
        else
        {
            stageState = StageState.Anomaly;
            if (randStage_max < 8)
            {
                randStage_max++;
            }
        }
    }


    // ���ӿ��� ó��
    public void EndGame()
    {
        
    }
}
