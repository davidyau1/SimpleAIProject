using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables
    public Text scoreText;
    public int score = 0;
    public int highScore;
    public Text highScoreText;

    //increase score by on when called
    public void IncreaseScore()
    {
        score++;
    }
    public void GameOver()
    {
       score=0;
    }

    //updates the UI for score
    private void Update()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }
}
