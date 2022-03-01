using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip[] audioClips;

    public void playSound(int sound)
    {
        audio.PlayOneShot(audioClips[sound]);
    }
}
