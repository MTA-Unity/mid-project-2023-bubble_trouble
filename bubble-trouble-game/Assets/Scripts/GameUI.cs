using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private int _currentLives;
    private bool _IsEnable = true;
    private GameObject _scoreTextGameObject;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;

    public static GameUI Instance { get; private set; }

    void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if ((ScoreManager.Instance != null) && (GameManager.Instance != null))
        {
            Start();
        }
    }

    void Start()
    {
        updateScore();
        updateLives();

    }

    public void updateScore()
    {
        _scoreTextGameObject = GameObject.FindWithTag("scoreText");

        if (_scoreTextGameObject != null)
        {
            scoreText = _scoreTextGameObject.GetComponent<TextMeshProUGUI>();
            scoreText.text = ScoreManager.Instance.GetCurrentScore().ToString();
        }

    }

    public void updateLives()
    {
        _currentLives = GameManager.Instance.GetLivesCount();
        _livesImage.sprite = _liveSprites[_currentLives];
    }

    public bool IsAudioEnable()
    {
        return _IsEnable;
    }

    public void SetAudioEnable(bool audio)
    {
        _IsEnable = audio;
    }
}
