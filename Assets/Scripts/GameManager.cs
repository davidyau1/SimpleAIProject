using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables
    public Text scoreText;
    public int score = 0;

    //increase score by on when called
    public void IncreaseScore()
    {
        score++;
    }

    //updates the UI for score
    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
