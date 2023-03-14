using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFX : MonoBehaviour
{

    new AudioSource audio;

    public static AudioFX instance;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

        if (instance == null) instance = this;
    }


    public void Play(AudioClip clip, float volume = 1)
    {
        if (clip != null) audio.PlayOneShot(clip, volume);
    }
}
