using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] public Transform player;

    void Update()
    {
        transform.LookAt(player);

        Vector3 fixedRotation = transform.eulerAngles;
        fixedRotation.x = 20;
        transform.eulerAngles = fixedRotation;
    }
}
