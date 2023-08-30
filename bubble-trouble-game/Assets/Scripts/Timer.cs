using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _levelTimeInSeconds = 60f; // Starting time in seconds
    
    private float _remainingTime; // Current time remaining
    private bool _isTimerRunning = true; // Flag to control whether the timer is running
    private int previousSecond = -1;

    private void Awake()
    {
        _remainingTime = _levelTimeInSeconds;
    }

    void Update()
    {
        if (_isTimerRunning)
        {
            // Update the timer if it's running
            _remainingTime -= Time.deltaTime;
            
            int currentSecond = Mathf.FloorToInt(_remainingTime);
            if (currentSecond != previousSecond)
            {
                previousSecond = currentSecond;
                Debug.Log("Time remaining: " + currentSecond + " seconds");
                Debug.Log("Number of balls in Scene (Timer): " + GameManager.Instance.GetBallsInScene());
            }

            if (_remainingTime < 0)
            {
                GameEvents.Instance.TriggerTimeUpEvent();
            }
        }
    }
    
    // Restart timer to start level time
    public void RestartTimer()
    {
        _remainingTime = _levelTimeInSeconds;
    }
    
    // On/off timer run
    public void ToggleTimerRun()
    {
        _isTimerRunning = !_isTimerRunning;
    }
    
    // Get the remaining time left for level
    public float ShowRemainingTime()
    {
        return _remainingTime;
    }
}
