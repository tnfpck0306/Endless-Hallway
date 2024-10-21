using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyArrange : MonoBehaviour
{
    public Transform[] keySpaces; // Ű�� ��ġ�� transform
    [SerializeField] private int num;

    void Start()
    {
        Arrangement();
    }

    // ���Ű ��ġ ����
    private void Arrangement()
    {
        num = Random.Range(0, keySpaces.Length);

        if(GameManager.instance.anomalyNum == 14)
        {
            num = 15;
        }

        Vector3 index = keySpaces[num].position;

        // �繰�� �ȿ� ���踦 ��ġ�� ��
        if (num < 15)
        {
            index.x += 0.15f;
            index.z -= 0.1f;
        }

        index.y += 0.05f;
        transform.position = index;

    }

}
