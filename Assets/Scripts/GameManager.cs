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

    // �������� ����(�Ϲ�, �̻�����)
    public enum StageState
    {
        Normal,
        Anomaly
    }

    public StageState stageState;
    public Anomaly anomaly;

    private int randStage; // �̻����� Ȯ��
    private int randStage_max;
    public int stage; // ���� ��������
    public bool condition; // ���� ���� ���� ���� Ȯ��

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
        stageState = StageState.Normal;
        randStage_max = 5;
        stage = 0;
        condition = true;

        anomalyData = new List<int>(anomaly.anomalyList);
        anomalyNum = 0;
    }

    // �������� ���� ����
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

    // �����
    public void CompareAns(string choice)
    {
        // ������ ���
        if (choice.Equals(stageState.ToString()) && condition)
        {
            stage++; // �������� ����
            anomalyData.Remove(GameManager.instance.anomalyNum);
        }

        // ������ ���
        else
        {
            // �������� �ʱ�ȭ(0�������� ����)
            condition = true;
            stage = 0;
            randStage_max = 5;
            anomalyData = new List<int>(anomaly.anomalyList);
            anomalyNum = 0;
        }
    }


    // ���ӿ��� ó��
    public void EndGame()
    {
        
    }
}
