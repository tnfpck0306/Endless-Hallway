using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 천장 부품 이상현상 스크립트
/// </summary>
public class CeilingPartMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float moveSpeed = 0.5f; // 오브젝트 이동 속도
    public float detectionAngle = 30f; // 플레이어의 시야각
    public float maxDistance = 5f; // 오브젝트가 움직일 수 있는 최대 거리

    public AnomalyManager anomalyManager;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= maxDistance && anomalyManager.ceilingAnomalyOn)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(-player.forward, directionToPlayer);

            // 플레이어가 오브젝트를 바라보고 있는지 체크
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
        // 오브젝트의 현재 위치
        Vector3 currentPosition = transform.position;

        // 목표 위치의 y값을 오브젝트의 현재 y 값으로 고정
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, player.position.z);

        // 오브젝트가 위치로 이동
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed);
    }
}
