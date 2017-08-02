using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class ScoreCameraController : MonoBehaviour
{

    public AudioController AudioController;
    public GameObject Glass;

    public GameObject Bottle;

    public GameObject SummaryPanel;

    public GameObject PourRotator;

    public Scores Scores;

    public ScoreText ScoreText;

    public CapController capController;

    public Button restartButton;

    public Animator FoamAnimator;

    [SerializeField]
    private PourTransition pourTransition;

    private Camera camera;

    public Vector3 CameraStartPosition;

    private Vector3 bottleStartPosition;

    private bool coroutineEnded;
    

    public void Awake()
    {
        this.camera = this.GetComponent<Camera>();
        this.CameraStartPosition = this.camera.transform.position;

        this.bottleStartPosition = this.Bottle.transform.position;
    }

    // Use this for initialization
    public void OnEnable()
    {
        SwipeController.Swipe += Restart;
        PourRotator.transform.rotation = Quaternion.identity;
        StartCoroutine("CameraAnimation");
    }

    private void OnDisable()
    {
        SwipeController.Swipe -= Restart;
    }

    private IEnumerator CameraAnimation()
    {
        this.ScoreText.enabled = false;

        this.Bottle.transform.DOMove(this.bottleStartPosition, 0.5f);
        Vector3 glassPosition = new Vector3(
            this.Glass.transform.position.x,
            this.CameraStartPosition.y,
            this.CameraStartPosition.z);
        this.camera.transform.DOMove(glassPosition, 0.5f);
        yield return new WaitForSeconds(1.5f);

        this.SummaryPanel.SetActive(true);

        int gainedScore = this.Scores.GetSinglePourScore();

        if (gainedScore <= this.Scores.scoreOverfilled)
        {
            FoamAnimator.SetTrigger("Overfill");
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowOverfilled(true);
            
            AudioController.PlayOverfilled();
            this.camera.transform.DOShakePosition(1f, 35f);
            SwipeController.Swipe -= Restart;
            
            yield return new WaitForSeconds(1f);
            restartButton.gameObject.SetActive(true);
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 0);
        }
        else if (gainedScore >= this.Scores.scorePerfectGlass)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowSuperGlass(true);
            AudioController.PlaySuperglass();
            this.camera.transform.DOShakePosition(0.5f, 30f);
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 100);
        }
        else if (gainedScore >= this.Scores.scoreAlmostPerfect)
        {
            FoamAnimator.SetTrigger("Almost");
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowAlmostPerfect(true);
            
            AudioController.PlayAlmostPerfect();
            this.camera.transform.DOShakePosition(0.5f, 20f);
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, gainedScore);
        }
        else if (gainedScore >= this.Scores.scoreTolerably)
        {
            FoamAnimator.SetTrigger("Tolerably");
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowTolerably(true);
            
            AudioController.PlayTolerable();
            this.camera.transform.DOShakePosition(0.2f, 10f);
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, gainedScore);
        }
        else if (gainedScore >= this.Scores.scoreLame)
        {
            FoamAnimator.SetTrigger("Sad");
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowLame(true);
            
            AudioController.PlayLame();
            this.camera.transform.DOShakePosition(0.2f, 10f);

            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 1);
        }

        yield return new WaitForSeconds(0.5f);

        FoamAnimator.ResetTrigger("Overfill");
        FoamAnimator.ResetTrigger("Sad");
        FoamAnimator.ResetTrigger("Almost");
        FoamAnimator.ResetTrigger("Tolerably");

        this.ScoreText.enabled = true;

        this.coroutineEnded = true;
    }

    private void Restart(SwipeController.SwipeDirection swipeDirection)
    {
        if (!this.coroutineEnded) return;
        if (swipeDirection == SwipeController.SwipeDirection.Left)
        {
            StartCoroutine("RestartCoroutine");        
        }
    }

    private IEnumerator RestartCoroutine()
    {
        this.camera.transform.DOMove(this.CameraStartPosition, 1f);
        this.SummaryPanel.GetComponent<SummaryPanelController>().HideAll();
        this.SummaryPanel.SetActive(false);
        FoamAnimator.SetTrigger("Exit");
        //FoamAnimator.ResetTrigger("Exit");

        yield return new WaitForSeconds(0.5f);
        FoamAnimator.ResetTrigger("Exit");
        capController.Reset();
        yield return new WaitForSeconds(0.5f);
        this.pourTransition.enabled = true;
        this.Glass.GetComponent<GlassFill>().ResetFill();
        this.enabled = false;
    }
}