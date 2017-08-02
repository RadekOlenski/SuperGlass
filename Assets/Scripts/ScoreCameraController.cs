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

    [SerializeField]
    private PourTransition pourTransition;

    private Camera camera;

    private Vector3 cameraStartPosition;

    private Vector3 bottleStartPosition;

    private bool coroutineEnded;
    

    public void Awake()
    {
        this.camera = this.GetComponent<Camera>();
        this.cameraStartPosition = this.camera.transform.position;

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

        this.camera.transform.DOMove(this.cameraStartPosition, 0.5f);
        this.Bottle.transform.DOMove(this.bottleStartPosition, 0.5f);
        yield return new WaitForSeconds(1f);

        Vector3 glassPosition = new Vector3(
            this.Glass.transform.position.x,
            cameraStartPosition.y,
            this.cameraStartPosition.z);
        this.camera.transform.DOMove(glassPosition, 0.5f);

        yield return new WaitForSeconds(1f);

        this.SummaryPanel.SetActive(true);

        int gainedScore = this.Scores.GetSinglePourScore();

        if (gainedScore <= this.Scores.scoreOverfilled)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowOverfilled(true);
            AudioController.PlayOverfilled();
            SwipeController.Swipe -= Restart;
            
            yield return new WaitForSeconds(1f);
            restartButton.gameObject.SetActive(true);
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 0);
        }
        else if (gainedScore >= this.Scores.scorePerfectGlass)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowSuperGlass(true);
            AudioController.PlaySuperglass();
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 100);
        }
        else if (gainedScore >= this.Scores.scoreAlmostPerfect)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowAlmostPerfect(true);
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, gainedScore);
        }
        else if (gainedScore >= this.Scores.scoreTolerably)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowTolerably(true);
            AudioController.PlayTolerable();
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, gainedScore);
        }
        else if (gainedScore >= this.Scores.scoreLame)
        {
            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowLame(true);
            AudioController.PlayLame();
            yield return new WaitForSeconds(0.5f);

            this.SummaryPanel.GetComponent<SummaryPanelController>().ShowScoreText(true, 1);
        }

        yield return new WaitForSeconds(0.5f);

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
        this.camera.transform.DOMove(this.cameraStartPosition, 1f);
        this.SummaryPanel.GetComponent<SummaryPanelController>().HideAll();
        this.SummaryPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        capController.Reset();
        this.pourTransition.enabled = true;
        this.Glass.GetComponent<GlassFill>().ResetFill();
        this.enabled = false;
    }
}