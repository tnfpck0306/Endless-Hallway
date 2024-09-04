using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyArrange : MonoBehaviour
{
    public Transform[] keySpaces;
    [SerializeField] private int num;

    void Start()
    {
        Arrangement();
    }

    private void Arrangement()
    {
        num = Random.Range(0, keySpaces.Length);

        Vector3 index = keySpaces[num].position;

        // �繰�� �ȿ� ���踦 ��ġ�� ��
        if (num < 18)
        {
            index.x -= 0.15f;
            index.z += 0.1f;
        }

        index.y += 0.05f;
        transform.position = index;

    }

}
