using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerEvent : MonoBehaviour
{
    public ObjectRotate objectRotate;

    // Update is called once per frame
    void Update()
    {
        objectRotate.Rotation(-80f, gameObject.transform);
    }
}
