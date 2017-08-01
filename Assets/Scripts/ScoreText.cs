using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Scores Scores;
    
    void Update()
    {
        GetComponent<Text>().text = Scores.GetCurrentScore().ToString();
    }
}