using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scores", menuName = "Scores", order = 0)]
public class Scores : ScriptableObject
{
    public int scorePerfectGlass;
    public int scoreAlmostPerfect;
    public int scoreTolerably;
    public int scoreLame;
    public int scoreOverfilled;

    public float fillSpeed;
    public float endFillSpeed;
    public float endFillTime;

    private int highScore;
    private int currentScore;

    private int singlePourScore;

    public void AddScore(int value)
    {
        this.singlePourScore = value;
        currentScore += value;
        if (highScore < currentScore)
            highScore = currentScore;
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetSinglePourScore()
    {
        return this.singlePourScore;
    }
}
