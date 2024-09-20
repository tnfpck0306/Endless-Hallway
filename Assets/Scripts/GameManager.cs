using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ��������, ���¿���, �̻����� ������ �����ϴ� ���� �Ŵ���
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
    public int randStage; // �̻����� Ȯ��
    public int randStage_max;
    public int stage;

    public List<int> anomalyData;
    public int anomalyNum; // �̻����� ����

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
        // ù ���������� �������� ����
        if (stage == 0)
            stageState = StageState.Normal;

        else // �� �� ��������(����Ȯ�� : �̻����� 75% / ���� 25% <�̻����� �߻� �� ���� Ȯ�� ����> )
        {
            
            if (randStage > 3) // �������� ��(�̻������� ���� ��)
            {
                stageState = StageState.Normal;
                anomalyNum = 0;
                randStage_max = 5; // Ȯ�� ����
            }

            else // �̻������� ���� ��
            {
                stageState = StageState.Anomaly;
                anomalyNum = anomalyData[Random.Range(0, anomalyData.Count)];
                // ���� �������� ������ Ȯ�� ����(�̻����� Ȯ�� ����)
                if (randStage_max < 8)
                {
                    randStage_max++;
                }
            }
        }
    }


    // ���ӿ��� ó��
    public void EndGame()
    {
        
    }
}
