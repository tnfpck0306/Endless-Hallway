using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public PlayerRotate playerRotate;
    private AudioSource audioSource;
    private bool audioCheck;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRotate.playerState == PlayerRotate.rotateState.Walk && !audioCheck)
        {
            audioSource.Play();
            audioCheck = true;
        }
        else if(playerRotate.playerState != PlayerRotate.rotateState.Walk && audioCheck)
        {
            audioSource.Stop();
            audioCheck = false;
        }
    }
}
