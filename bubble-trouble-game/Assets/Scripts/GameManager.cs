using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Timer _timer;
    private int _ballsInScene = 0;
    private bool _waitingForBalls;
    private const int StartLives = 3;
    private int _livesCount;
    private float _currentScore = 0f;
    private String[] _sceneLevelsNames = new string[2] {"Level1", "Level2"};
    private int _currentLevelIndex = 0;
    
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
        
        // Subscribe to game events
        GameEvents.Instance.BallCreatedEvent.AddListener(OnBallCreated);
        GameEvents.Instance.BallDestroyedEvent.AddListener(OnBallDestroyed);
        GameEvents.Instance.LifeDecreaseEvent.AddListener(OnLifeDecrease);
        GameEvents.Instance.LifeIncrementEvent.AddListener(OnLifeIncrement);
        
        // Find all Ball objects by tag 
        _ballsInScene = GameObject.FindGameObjectsWithTag("Ball").Length;
        Debug.Log("Number of Balls in scene: " + _ballsInScene);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from ball events
        GameEvents.Instance.BallCreatedEvent.RemoveListener(OnBallCreated);
        GameEvents.Instance.BallDestroyedEvent.RemoveListener(OnBallDestroyed);
        GameEvents.Instance.LifeDecreaseEvent.RemoveListener(OnLifeDecrease);
        GameEvents.Instance.LifeIncrementEvent.RemoveListener(OnLifeIncrement);
    }

    void Update()
    {
        // Win condition - If there are no balls in the scene
        // TODO end of scene bool variable
        if (_ballsInScene == 0)
        {
          //TODO Go to next level
          Debug.Log("You Win!!!1");
          _currentLevelIndex++;
          if (_currentLevelIndex >= _sceneLevelsNames.Length)
          {
              SceneManager.LoadScene("MainMenu");
          }
          else
          {
              SceneManager.LoadScene(_sceneLevelsNames[_currentLevelIndex]);
          }
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
        _waitingForBalls = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // Find all Ball objects by tag 
        _ballsInScene = GameObject.FindGameObjectsWithTag("Ball").Length;
        Debug.Log("Number of Balls in scene: " + _ballsInScene);
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
    
    private void OnBallCreated()
    {
        Debug.Log("OnBallCreated");
        // When a ball is created in the scene, increment by 1 the balls counter
        _ballsInScene++;
        Debug.Log("Number of Balls in scene: " + _ballsInScene);
    }

    private void OnBallDestroyed()
    {
        Debug.Log("OnBallDestroyed");
        // When a ball is destroyed in the scene, decrease by 1 the balls counter
        _ballsInScene--;
        Debug.Log("Number of Balls in scene: " + _ballsInScene);
    }
    
    private void OnLifeDecrease()
    {
        // When a life of the player is lost, decrease by 1 the lives counter
        _livesCount--;
        Debug.Log("Remained lives: " + _livesCount);
    }
    
    private void OnLifeIncrement()
    {
        // When a life of the player is gained, increment by 1 the lives counter
        _livesCount++;
        Debug.Log("Remained lives: " + _livesCount);
    }
}
