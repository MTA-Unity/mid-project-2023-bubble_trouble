using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _ballsInScene = 0;
    private const int StartLives = 3;
    public int _livesCount;

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
        GameUI.Instance.updateLives();


        // Subscribe to game events
        GameEvents.Instance.ballCreatedEvent.AddListener(OnBallCreated);
        GameEvents.Instance.ballDestroyedEvent.AddListener(OnBallDestroyed);
        GameEvents.Instance.lifeDecreaseEvent.AddListener(OnLifeDecrease);
        GameEvents.Instance.lifeIncrementEvent.AddListener(OnLifeIncrement);
        GameEvents.Instance.timeUpEvent.AddListener(OnTimeUp);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from ball events
        GameEvents.Instance.ballCreatedEvent.RemoveListener(OnBallCreated);
        GameEvents.Instance.ballDestroyedEvent.RemoveListener(OnBallDestroyed);
        GameEvents.Instance.lifeDecreaseEvent.RemoveListener(OnLifeDecrease);
        GameEvents.Instance.lifeIncrementEvent.RemoveListener(OnLifeIncrement);
        GameEvents.Instance.timeUpEvent.RemoveListener(OnTimeUp);
    }

    private void OnBallCreated()
    {
        Debug.Log("OnBallCreated");
        // When a ball is created in the scene, increment by 1 the balls counter
        _ballsInScene++;
        Debug.Log("Number of Balls in scene: " + _ballsInScene);

        GameUI.Instance.updateScore();
    }

    private void OnBallDestroyed()
    {
        Debug.Log("OnBallDestroyed");
        // When a ball is destroyed in the scene, decrease by 1 the balls counter
        _ballsInScene--;
        Debug.Log("Number of balls in scene: " + _ballsInScene);
    }

    private void OnLifeDecrease()
    {
        // When a life of the player is lost, decrease by 1 the lives counter
        _livesCount--;
        GameUI.Instance.updateLives();

        // If there are no lives remained, you lost the game
        if (_livesCount == 0)
        {
            Debug.Log("You Lost The Game!");
            Debug.Log("Your score for the game: " + ScoreManager.Instance.GetCurrentScore());
            
            var scoreHighScore = UpdateHighScore();
            ShowFinishGameScreen(false, scoreHighScore.Item1, scoreHighScore.Item2);
            
            ScoreManager.Instance.ResetScore();
            _livesCount = StartLives;
        }
        else
        {
            Debug.Log("ResetLevel");
            Debug.Log("Remained Lives: " + _livesCount);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void OnLifeIncrement()
    {
        // When a life of the player is gained, increment by 1 the lives counter
        _livesCount++;
        GameUI.Instance.updateLives();
    }

    private void OnTimeUp()
    {
        Debug.Log("Time is Up!");
        GameEvents.Instance.TriggerLifeDecreaseEvent();
    }

    public bool HasWonLevel(bool hasNextBall)
    {
        return _ballsInScene == 1 && !hasNextBall;
    }

    public void ClearLevelWon()
    {
        // If there are no balls in the scene - you won the level
        Debug.Log("Level Cleared!");
        Debug.Log("Number of balls in scene: " + _ballsInScene);
        
        // Add score according to remained time
        var timer = FindObjectOfType<Timer>();
        ScoreManager.Instance.AddRemainedTimeScore(timer.ShowRemainingTime());
        
        // Go to next level if there are any left
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there are no more levels - You won the game
            // Check for high score and save it
            Debug.Log("You Won The Game!");
            var scoreHighScore = UpdateHighScore();
            ShowFinishGameScreen(true, scoreHighScore.Item1, scoreHighScore.Item2);
            
            ScoreManager.Instance.ResetScore();
            _livesCount = StartLives;
        }
    }

    private (int, bool) UpdateHighScore()
    {
        int score = ScoreManager.Instance.GetCurrentScore();
        bool highScore = score > PlayerPrefs.GetInt("HighScore", 0);
        
        if (highScore)
        {
            Debug.Log("You got high score! " + score);
            PlayerPrefs.SetInt("HighScore", score);
        }

        return (score, highScore);
    }

    private void ShowFinishGameScreen(bool win, int score, bool highScore)
    {
        var gameFinishScreenManager = FindObjectOfType<GameFinishScreenManager>();
        gameFinishScreenManager.Setup(win, score, highScore);
    }

    public int GetLivesCount()
    {
        return _livesCount;
    }
    
}
