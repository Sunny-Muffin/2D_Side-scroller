using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private int score;
    private int highScore;

    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "score " + score;
        highScoreText.text = "highscore " + highScore;
    }

    public void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            // добавить функцию сохранения счета
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        scoreText.text = "score " + score;
        highScoreText.text = "highscore " + highScore;
    }

    public void ResetHighScore()
    {
        highScore = 0;
        highScoreText.text = "highscore " + highScore;
    }
}
