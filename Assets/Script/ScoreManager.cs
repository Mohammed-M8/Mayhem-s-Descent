using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentScore;
    public int totalScore;
    public TMP_Text scoreText;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        DisplayScore();
    }

    public void CollectCollectable()
    {
        currentScore++;
        DisplayScore();

        if(currentScore >= totalScore)
        {
            gameManager.GameWon();
        }
    }

    public void DisplayScore()
    {
        Debug.Log("Current Score is " + currentScore + "out of " + totalScore);
        scoreText.text = currentScore.ToString();
    }
}
