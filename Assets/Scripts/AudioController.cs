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
    public AudioClip Overfilled;
    public AudioClip Lame;
    public AudioClip Tolerable;

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

    public void PlayOverfilled()
    {
        this.AudioSource.clip = this.Overfilled;
        this.AudioSource.Play();
    }

    public void PlayLame()
    {
        this.AudioSource.clip = this.Lame;
        this.AudioSource.Play();
    }

    public void PlayTolerable()
    {
        this.AudioSource.clip = this.Tolerable;
        this.AudioSource.Play();
    }

    public void StopAudio()
    {
        this.AudioSource.Stop();
    }
}