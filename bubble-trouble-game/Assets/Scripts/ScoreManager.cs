using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private const int TimeScoreMultiplier = 10; // Score multiplier for time remaining
    private int _currentScore = 0;
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

    public void AddRemainedTimeScore(float remainedTime)
    {
        Debug.Log("Remained time to add score: " + remainedTime);
        int addedScore = Mathf.FloorToInt(remainedTime) * TimeScoreMultiplier;
        Debug.Log("Added score from time: " + addedScore);
        _currentScore += addedScore;
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public float GetCurrentScore()
    {
        return _currentScore;
    }
}
