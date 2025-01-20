using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindKeyTimer : MonoBehaviour
{

    public Transform player; // 플레이어의 Transform
    public float detectionAngle = 45f; // 플레이어의 시야각
    public ClubroomTrigger trigger;

    private float timer = 10f;
    private bool gameOver = false;
    [SerializeField]private TextMeshPro timerText;

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(-player.forward, directionToPlayer);

        // 플레이어가 오브젝트를 바라보고 있는지 체크
        if (angleToPlayer < detectionAngle / 2f)
        {

        }
        else
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();
        }

        if(timer <= 0 && !gameOver)
        {
            gameOver = true;
            GameManager.instance.EndGame();
        }
    }
}
