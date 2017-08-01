using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Scores scores;

    [SerializeField] private PourTransition pt;
    [SerializeField] private GlassFill gf;

    private bool scoreAdded = false;

    private void Update()
    {
        if (pt.PourStarted)
        {
            scoreAdded = false;
            gf.StartFill(scores.fillSpeed);
        }
        if (pt.PourEnding && !scoreAdded)
        {
            float? score = gf.EndFill(scores.endFillSpeed, scores.endFillTime);
            if (score.HasValue)
            {
                scores.AddScore(Mathf.RoundToInt(Mathf.PingPong((float)score, 1) * scores.scorePerfectGlass));
                scoreAdded = true;
                Debug.Log("Score: " + scores.GetCurrentScore());
            }
        }
    }

    private void OnDestroy()
    {
        scores.ResetCurrentScore();
    }
}
