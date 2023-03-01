using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioFXturning : MonoBehaviour
{

    AudioSource audio;

    [SerializeField] AudioClip turning;
    [SerializeField] AudioClip happy;
    [SerializeField, Range(0, 1)] float happyVolume = 0.8f;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayTuring()
    {
        if (turning != null) audio.PlayOneShot(turning);
    }

    public void PlayHappy()
    {
        if (happy != null) audio.PlayOneShot(happy, happyVolume);
    }


}
