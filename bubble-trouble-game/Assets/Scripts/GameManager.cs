using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Time timer;
    private int _ballsInScene;
    [SerializeField] private const int _startLives = 3;
    private int _livesCount;
    [SerializeField] private float _gameTimeInSeconds = 60f;
    
    private float _remainingTime;
    
    public static GameManager Instance { get; private set; }
    void Start()
    {
        _livesCount = _startLives;
        _ballsInScene = GameObject.FindGameObjectsWithTag("Ball").Length;
        
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

    void Update()
    {
        if (_ballsInScene == 0)
        {
          //TODO Go to main menu (Win UI)
          Debug.Log("You WIn!!!1");
        }

        if (_livesCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // if (timer.remainingfTime < 0)
        // {
        //     livesCount -= 1;
        // }
    }
    
    private void ResetGame()
    {
        // Timer.reset();
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void ResetLevel()
    {
        // Timer.reset();
        
    }

    public void UpdateBallsInScene(int numToUpdate)
    {
        _ballsInScene += numToUpdate;
    }
    
    public void UpdateLivesCount(int numToUpdate)
    {
        _livesCount += numToUpdate;

        if (numToUpdate < 0)
        {
            ResetGame();
        }
    }
}
