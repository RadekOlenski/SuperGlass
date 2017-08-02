using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip BottleOpen;
    public AudioClip BottleFill;
    public AudioClip BottleOverfill;
    public AudioClip Superglass;

    public void PlayBottleOpen()
    {
        this.AudioSource.clip = this.BottleOpen;
        this.AudioSource.Play();
    }

    public void PlayBottleFill()
    {
        this.AudioSource.clip = this.BottleFill;
        this.AudioSource.Play();
    }

    public void PlayBottleOverfill()
    {
        this.AudioSource.clip = this.BottleOverfill;
        this.AudioSource.Play();
    }

    public void PlaySuperglass()
    {
        this.AudioSource.clip = this.Superglass;
        this.AudioSource.Play();
    }

    public void StopAudio()
    {
        this.AudioSource.Stop();
    }
}