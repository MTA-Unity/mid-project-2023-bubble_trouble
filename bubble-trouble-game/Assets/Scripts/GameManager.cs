using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _ballsInScene = 0;
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
        
        // Subscribe to game events
        GameEvents.Instance.BallCreatedEvent.AddListener(OnBallCreated);
        GameEvents.Instance.BallDestroyedEvent.AddListener(OnBallDestroyed);
        GameEvents.Instance.LifeDecreaseEvent.AddListener(OnLifeDecrease);
        GameEvents.Instance.LifeIncrementEvent.AddListener(OnLifeIncrement);
        GameEvents.Instance.TimeUpEvent.AddListener(OnTimeUp);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from ball events
        GameEvents.Instance.BallCreatedEvent.RemoveListener(OnBallCreated);
        GameEvents.Instance.BallDestroyedEvent.RemoveListener(OnBallDestroyed);
        GameEvents.Instance.LifeDecreaseEvent.RemoveListener(OnLifeDecrease);
        GameEvents.Instance.LifeIncrementEvent.RemoveListener(OnLifeIncrement);
        GameEvents.Instance.TimeUpEvent.RemoveListener(OnTimeUp);
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
        Debug.Log("Number of balls in scene: " + _ballsInScene);
    }
    
    private void OnLifeDecrease()
    {
        // When a life of the player is lost, decrease by 1 the lives counter
        _livesCount--;
        
        // If there are no lives remained, you lost the game
        if (_livesCount == 0)
        {
            Debug.Log("You Lost The Game!");
            _livesCount = StartLives;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("ResetLevel");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    private void OnLifeIncrement()
    {
        // When a life of the player is gained, increment by 1 the lives counter
        _livesCount++;
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

    public int GetBallsInScene()
    {
        return _ballsInScene;
    }

    public void ClearLevel()
    {
        Debug.Log("Level Cleared!");
        Debug.Log("Number of balls in scene: " + _ballsInScene);
        // If there are no balls in the scene - you won the level
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there are no more levels - You won the game: Go to main menu
            Debug.Log("You Won The Game!");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
