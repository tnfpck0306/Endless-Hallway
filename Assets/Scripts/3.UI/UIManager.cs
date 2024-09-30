using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text stageStateText;
    public Text stageText;
    public Text anomalyText;
    public Text playerStateText;

    public PlayerMovement playerMovement;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerStateText.text = $"PlayerState : {playerMovement.playerState}";
        stageStateText.text = $"Stage State : {GameManager.instance.anomalyNum}{GameManager.instance.stageState}";
        stageText.text = $"Stage : {GameManager.instance.stage}";
    }
}
