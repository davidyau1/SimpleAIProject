using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public void IncreaseScore()
    {
        score++;
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
