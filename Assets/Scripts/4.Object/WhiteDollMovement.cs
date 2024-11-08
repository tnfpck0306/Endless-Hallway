using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDollMovement : MonoBehaviour
{
    public float walkDuration = 10f;
    public float turnDuration = 6f;
    private Quaternion startRotation;
    private Quaternion endRotation;

    private bool turnCheck;
    private float walkElapsedTime = 0f;
    private float turnElapsedTime = 0f;

    private void Start()
    {
        turnCheck = true;
        startRotation = transform.rotation;
        endRotation = transform.rotation * Quaternion.Euler(0f, 185f, 0f);
    }

    private void Update()
    {
        if (walkElapsedTime < walkDuration)
        {
            walkElapsedTime += Time.deltaTime;
        }
        else 
        {
            if(!turnCheck)
            {
                turnCheck = true;
                startRotation = transform.rotation;
            }

            if (turnElapsedTime < turnDuration)
            {
                turnElapsedTime += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, turnElapsedTime / turnDuration);
            }
            else
            {
                turnCheck = false;
                endRotation = transform.rotation * Quaternion.Euler(0f, 175f, 0f);
                walkElapsedTime = 0f;
                turnElapsedTime = 0f;
            }
        }
    }
}
