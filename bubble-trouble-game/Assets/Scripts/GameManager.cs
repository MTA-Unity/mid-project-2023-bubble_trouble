using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Timer _timer;
    private int _ballsInScene;
    private const int StartLives = 3;
    private int _livesCount;
    public static GameManager Instance { get; private set; }
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
        _livesCount = StartLives;
        Debug.Log("Current lives: " + _livesCount);
        _timer = FindObjectOfType<Timer>();
        _ballsInScene = GameObject.FindGameObjectsWithTag("Ball").Length;
    }

    void Update()
    {
        // Win condition - If there are no balls in the scene
        if (_ballsInScene == 0)
        {
          //TODO Go to main menu (Win UI)
          Debug.Log("You Win!!!1");
        }
        
        // Timer lose life condition - If the time has run out decrease a life
        if (_timer.ShowRemainingTime() < 0)
        {
            UpdateLivesCount(-1);
        }
    }
    
    private void ResetGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetLevel()
    {
        _timer.RestartTimer();
        Debug.Log("Lives: " + _livesCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // TODO Reset player and balls to staring position
    }

    public void UpdateBallsInScene(int numToUpdate)
    {
        _ballsInScene += numToUpdate;
    }
    
    public void UpdateLivesCount(int numToUpdate)
    {
        _livesCount += numToUpdate;
        
        // If the player has run out of lives, exit level to main menu
        if (_livesCount == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        // Check if a life has been decreased. If so, restart the level
        else if (numToUpdate == -1)
        {
            ResetLevel();
        }
    }
}
