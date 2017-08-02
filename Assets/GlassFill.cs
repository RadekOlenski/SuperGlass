using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GlassFill : MonoBehaviour
{
    [SerializeField]
    private GameObject foam;

    [SerializeField]
    private GameObject moreFoam;

    [SerializeField]
    private GameObject overFoam;

    [SerializeField]
    private ParticleSystem foamParticle;

    public AudioController AudioController;

    private float fillLevel = 0;

    private Vector3 startFoamPos;

    private float timer = 0;

    private bool audioPlaying;

    private bool overfillAudioPlaying;

    [SerializeField]
    private SpriteRenderer sr;

    void Start()
    {
        startFoamPos = foam.transform.localPosition;
        startFoamPos.y = 0;
        foam.transform.position = startFoamPos;
        moreFoam.transform.position = startFoamPos;
        overFoam.transform.position = startFoamPos;
        timer = 0;
    }

    void Update()
    {
        sr.gameObject.transform.localScale = new Vector3(1, Mathf.Clamp01(fillLevel), 1);

        if (this.fillLevel >= 1.1)
        {
            if (!this.overfillAudioPlaying)
            {
                this.AudioController.PlayBottleOverfill();
                this.overfillAudioPlaying = true;
            }
        }

        if (fillLevel >= 1)
        {
            overFoam.transform.localPosition = Vector3.Lerp(
                startFoamPos,
                new Vector3(startFoamPos.x, startFoamPos.y + 62, startFoamPos.z),
                fillLevel - 0.05f);
            if (fillLevel >= 1.1f && !foamParticle.isPlaying)
            {
                foamParticle.Play();
            }
        }

        // if(updatePosition)
        // overFoam.transform.position = foam.transform.position;
    }

    public void StartFill(float fillSpeed)
    {
        fillLevel = fillLevel + Time.deltaTime * fillSpeed;
        foam.transform.localPosition = Vector3.Lerp(
            startFoamPos,
            new Vector3(startFoamPos.x, startFoamPos.y + 59, startFoamPos.z),
            fillLevel);
        moreFoam.transform.localPosition = Vector3.Lerp(
            startFoamPos,
            new Vector3(startFoamPos.x, startFoamPos.y + 52, startFoamPos.z),
            fillLevel);
        timer = 0;
    }

    public float? EndFill(float endSpeed, float endTime)
    {

        if (timer <= endTime)
        {
            fillLevel = fillLevel + Time.deltaTime * endSpeed;
            foam.transform.localPosition = Vector3.Lerp(
                startFoamPos,
                new Vector3(startFoamPos.x, startFoamPos.y + 59, startFoamPos.z),
                fillLevel);
            moreFoam.transform.localPosition = Vector3.Lerp(
                startFoamPos,
                new Vector3(startFoamPos.x, startFoamPos.y + 52, startFoamPos.z),
                fillLevel);
            timer += Time.deltaTime;

            return null;
        }
        else
        {
            AudioController.StopAudio();
            return fillLevel;
        }
    }

    public void ResetFill()
    {
        foam.transform.position = startFoamPos;
        moreFoam.transform.position = startFoamPos;
        overFoam.transform.position = startFoamPos;
        fillLevel = 0;
        this.overfillAudioPlaying = false;
    }
}