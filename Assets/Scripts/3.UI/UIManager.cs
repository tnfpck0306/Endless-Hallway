using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    public Text stageStateText;
    public Text stageText;
    public Text anomalyText;


    void Start()
    {
        anomalyText.text = $"Anomaly Percent : {(float)400 / GameManager.instance.randStage_max}%";
    }

    // Update is called once per frame
    void Update()
    {
        stageStateText.text = $"Stage State : {GameManager.instance.randStage}{GameManager.instance.stageState}";
        stageText.text = $"Stage : {GameManager.instance.stage}";
        anomalyText.text = $"Anomaly Percent : {(float)400 / GameManager.instance.randStage_max}%";
    }
}
