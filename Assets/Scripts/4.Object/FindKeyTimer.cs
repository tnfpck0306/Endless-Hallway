using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// ���� ã�� �̻�����(19��) Ÿ�̸�
/// </summary>
public class FindKeyTimer : MonoBehaviour
{

    public Transform player; // �÷��̾��� Transform
    public float detectionAngle = 45f; // �÷��̾��� �þ߰�
    public ClubroomTrigger trigger;

    private GameObject monster;
    private float timer = 31f;
    private bool gameOver = false;
    [SerializeField]private TextMeshPro timerText;

    private void Start()
    {
        monster = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(-player.forward, directionToPlayer);

        // �÷��̾ ������Ʈ�� �ٶ󺸰� �ִ��� üũ
        if (angleToPlayer < detectionAngle / 2f)
        {

        }
        else
        {
            timer -= Time.deltaTime;
            if ((int)timer % 5 == 0)
            {
                GetComponent<AudioSource>().Play();
                timerText.text = ((int)timer).ToString();
            }

            if (!monster.activeSelf)
            {
                monster.SetActive(true);
            }
        }

        if(timer <= 0 && !gameOver)
        {
            gameOver = true;
            GameManager.instance.EndGame();
        }
    }
}
