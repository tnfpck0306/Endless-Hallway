using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static PlayerMovement;

public class UIManager : MonoBehaviour
{
    public Text stageStateText;
    public Text stageText;
    public Text anomalyText;
    public Text playerStateText;

    public PlayerMovement playerMovement;

    void Start()
    {
        anomalyText.text = $"Anomaly Percent : {(float)400 / GameManager.instance.randStage_max}%";
    }

    // Update is called once per frame
    void Update()
    {
        playerStateText.text = $"PlayerState : {playerMovement.playerState}";
        stageStateText.text = $"Stage State : {GameManager.instance.randStage}{GameManager.instance.stageState}";
        stageText.text = $"Stage : {GameManager.instance.stage}";
        anomalyText.text = $"Anomaly Percent : {(float)400 / GameManager.instance.randStage_max}%";
    }
}
