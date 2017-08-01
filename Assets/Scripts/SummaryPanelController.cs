using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SummaryPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject overfilled;

    [SerializeField]
    private GameObject superGlass;

    [SerializeField]
    private GameObject almostPerfect;

    [SerializeField]
    private GameObject tolerably;

    [SerializeField]
    private GameObject lame;

    [SerializeField]
    private GameObject scoreText;

    public void ShowSuperGlass(bool value)
    {
        superGlass.SetActive(value);
    }

    public void ShowOverfilled(bool value)
    {
        this.overfilled.SetActive(value);
    }

    public void ShowAlmostPerfect(bool value)
    {
        this.almostPerfect.SetActive(value);
    }

    public void ShowTolerably(bool value)
    {
        this.tolerably.SetActive(value);
    }

    public void ShowLame(bool value)
    {
        this.lame.SetActive(value);
    }

    public void ShowScoreText(bool value, int gainedScore = 0)
    {
        this.scoreText.GetComponent<Text>().text = gainedScore.ToString();
        this.scoreText.SetActive(value);
    }

}