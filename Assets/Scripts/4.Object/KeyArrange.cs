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

        if (num < 16)
        {
            index.x -= 0.15f;
            index.y += 0.05f;
            index.z += 0.1f;
        }

        transform.position = index;

    }

}
