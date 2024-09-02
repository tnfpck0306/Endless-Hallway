using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // 오디오 믹서, 오디오의 타입별로 사운드를 조절할 수 있도록 한다.
    [SerializeField] private AudioMixer mAudioMixer;

    // 사전에 미리 로드하여 사용할 클립들
    [SerializeField] private AudioClip[] mPreloadClips;


    
}
