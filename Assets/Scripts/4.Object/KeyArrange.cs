using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyArrange : MonoBehaviour
{
    public Transform[] keySpaces; // 키가 배치될 transform
    [SerializeField] private int num;

    void Start()
    {
        Arrangement();
    }

    // 블루키 배치 로직
    private void Arrangement()
    {
        num = Random.Range(0, keySpaces.Length);

        if(GameManager.instance.anomalyNum == 14)
        {
            num = 15;
        }

        Vector3 index = keySpaces[num].position;

        // 사물함 안에 열쇠를 배치할 때
        if (num < 15)
        {
            index.x += 0.15f;
            index.z -= 0.1f;
        }

        index.y += 0.05f;
        transform.position = index;

    }

}
