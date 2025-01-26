using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ƹ��� Ű ��ġ ��ũ��Ʈ
/// </summary>
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
        num = Random.Range(0, keySpaces.Length - 1);

        if(GameManager.instance.anomalyNum == 14)
        {
            num = 28;
        }

        Vector3 index = keySpaces[num].position;
        Quaternion indexRotation = keySpaces[num].rotation;

        // �繰�� �ȿ� ���踦 ��ġ�� ��
        if (num < 15)
        {
            index.x += 0.15f;
            index.z -= 0.1f;
        }
        else if(num < 28){
            index.y -= 0.05f;
            transform.rotation = indexRotation;
        }

        index.y += 0.05f;
        transform.position = index;

    }

}
