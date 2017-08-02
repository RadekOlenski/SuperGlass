using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioController AudioController;

    [SerializeField]
    private Scores scores;

    [SerializeField]
    private PourTransition pt;

    [SerializeField]
    private GlassFill gf;

    private bool scoreAdded = false;

    private float timer;

    private bool audioPlaying;

    private float wait = 1;

    private void Update()
    {
        if (CapController.isBottleOpened &&  pt.PourStarted)
        {
            scoreAdded = false;
            if (!this.audioPlaying)
            {
                AudioController.PlayBottleFill();
                this.audioPlaying = true;
            }
            if (timer >= wait)
            {
                gf.StartFill(scores.fillSpeed);
            }

            timer += Time.deltaTime;
        }

        if (pt.PourEnding && !scoreAdded)
        {
            timer = 0;
            float? score = gf.EndFill(scores.endFillSpeed, scores.endFillTime);
            if (score.HasValue)
            {
                if (score >= 1.1)
                {
                    scores.AddScore(0);
                }
                else if (score >= 1.0)
                {
                    scores.AddScore(100);
                }
                else if (score < 0.7)
                {
                    scores.AddScore(1);
                }
                else
                {
                    scores.AddScore(Mathf.RoundToInt(Mathf.PingPong((float)score, 1) * scores.scorePerfectGlass));
                }

                scoreAdded = true;
                Debug.Log("Score: " + scores.GetCurrentScore());
                this.pt.PourEnding = false;
                this.pt.enabled = false;
                this.audioPlaying = false;
            }
        }
    }

    private void OnDestroy()
    {
        scores.ResetCurrentScore();
    }
}