using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;  
    [SerializeField] TextMeshProUGUI scoreText;  
    [SerializeField] int score = 0;  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();  
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Bee: " + score;
        }
    }
}