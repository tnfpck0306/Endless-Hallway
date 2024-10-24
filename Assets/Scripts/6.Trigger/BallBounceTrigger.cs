using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounceTrigger : MonoBehaviour
{
    public BallMovement ballMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ballMovement.startMove();
        }
    }
}
