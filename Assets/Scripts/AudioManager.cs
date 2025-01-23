using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // 사전에 미리 로드하여 사용할 클립들
    public AudioClip[] preloadClips;

    public AudioSource[] sfx;

}
