using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVX : MonoBehaviour
{

    AudioSource audio;

    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip softShot;
    [SerializeField] AudioClip boom;

    public static AudioVX instance;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (instance == null) instance = this;
    }

 
    public void PlayHit()
    {
        if (hit != null) audio.PlayOneShot(hit);
    }

    public void PlayShoot(bool soft)
    {
        if (soft)
        {
            if (softShot != null) audio.PlayOneShot(softShot);
        }
        else if (shoot != null) audio.PlayOneShot(shoot);
    }

    public void PlayBoom()
    {
        if (boom != null) audio.PlayOneShot(boom);
    }


}
