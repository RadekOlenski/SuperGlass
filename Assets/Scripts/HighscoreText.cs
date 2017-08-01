using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{
    public Scores Scores;

    void Update()
    {
        GetComponent<Text>().text = Scores.GetHighScore().ToString();
    }
}