using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class CapController : MonoBehaviour
{
    public static bool isBottleOpened = false;
    public ParticleSystem particle;

    public AudioController AudioController;
    public GameObject Camera;

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
                this.Camera.transform.DOShakePosition(0.5f, 3f);
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
