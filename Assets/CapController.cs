using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapController : MonoBehaviour
{
    public static bool isBottleOpened = false;
    public ParticleSystem particle;

    public AudioController AudioController;

    private bool audioPlayed;

    public void OnEnable()
    {
        SwipeController.Swipe += OpenBottle;
    }

    private void OnDisable()
    {
        SwipeController.Swipe -= OpenBottle;
    }

    void OpenBottle(SwipeController.SwipeDirection dir)
    {
        if (dir == SwipeController.SwipeDirection.Up)
        {
            GetComponent<Animator>().SetTrigger("Open");
            particle.Play();
            isBottleOpened = true;
            if (!this.audioPlayed)
            {
                AudioController.PlayBottleOpen();
                this.audioPlayed = true;
            }
        }
    }

    public void Reset()
    {
        isBottleOpened = false;
        GetComponent<Animator>().ResetTrigger("Open");
        GetComponent<Animator>().SetTrigger("Exit");
        particle.Stop();
        this.audioPlayed = false;
    }
}
