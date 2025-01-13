using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// õ�� ��ǰ �̻����� ��ũ��Ʈ
/// </summary>
public class CeilingPartMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float moveSpeed = 0.5f; // ������Ʈ �̵� �ӵ�
    public float detectionAngle = 30f; // �÷��̾��� �þ߰�
    public float maxDistance = 5f; // ������Ʈ�� ������ �� �ִ� �ִ� �Ÿ�

    public AnomalyManager anomalyManager;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= maxDistance && anomalyManager.ceilingAnomalyOn)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(-player.forward, directionToPlayer);

            // �÷��̾ ������Ʈ�� �ٶ󺸰� �ִ��� üũ
            if (angleToPlayer < detectionAngle / 2f)
            {

            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // ������Ʈ�� ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ��ǥ ��ġ�� y���� ������Ʈ�� ���� y ������ ����
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, player.position.z);

        // ������Ʈ�� ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed);
    }
}
