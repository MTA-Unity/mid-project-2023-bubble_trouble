using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private const int TimeScoreMultiplier = 5; // Score multiplier for time remaining
    private float _currentScore = 0f;
    private Dictionary<string, int> _ballSizeToScore;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {   
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Define the mapping of ball sizes to scores
        _ballSizeToScore = new Dictionary<string, int>()
        {
            { "Ball1", 300 },
            { "Ball2", 250 },
            { "Ball3", 200 },
            { "Ball4", 150 }
        };
    }

    public void AddBallHitScore(string ballSize)
    {
        _currentScore += _ballSizeToScore[ballSize];
        GameUI.Instance.updateScore();

        Debug.Log("Added score: " + _ballSizeToScore[ballSize]);
        Debug.Log("Current score: " + _currentScore);
    }

    void AddRemainTimeScores()
    {
        
    }

    public void ResetScore()
    {
        _currentScore = 0f;
    }

    public float GetCurrentScore()
    {
        return _currentScore;
    }
}
