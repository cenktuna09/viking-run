using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource aSource;

    [Header("Sounds")]
    public AudioClip shutterSound;
    public AudioClip gateSound;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlayCameraSound()
    {
        aSource.PlayOneShot(shutterSound);
    }

    public void PlayGateSound()
    {
        aSource.PlayOneShot(gateSound);
    }
}
