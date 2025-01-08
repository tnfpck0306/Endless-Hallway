using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 상호작용 하는 아이템 소지 여부를 확인하는 역할
/// </summary>
public class PlayerInven : MonoBehaviour
{
    public bool blueKey;
    public bool redKey;

    void Start()
    {
        blueKey = false;
        redKey = false;
    }
}
