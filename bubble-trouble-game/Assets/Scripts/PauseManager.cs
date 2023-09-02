using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public List<Sprite> spriteList;
    private int _curSpriteID = 0;
    private Image _imageComp;
    private GameObject _imageCompGameObject;

    void Start()
    {
        SetImage();
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Game resumed");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        Debug.Log("Game paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    public void ChangeImage()
    {
        _imageCompGameObject = GameObject.FindWithTag("Sound");

        if (_imageCompGameObject != null)
            _imageComp = _imageCompGameObject.GetComponent<Image>();

        _curSpriteID++;
        if (_curSpriteID == spriteList.Count)
        {
            Debug.Log("Sound unmute");
            GameUI.Instance.SetAudioEnable(true);
            _curSpriteID = 0;
        }
        else
        {
            Debug.Log("Sound mute");
            GameUI.Instance.SetAudioEnable(false);
        }
        _imageComp.sprite = spriteList[_curSpriteID];
    }

    private void SetImage()
    {
        Debug.Log("SetImage " + GameUI.Instance.IsAudioEnable());

        _imageCompGameObject = GameObject.FindWithTag("Sound");

        if (_imageCompGameObject != null)
        {
            _imageComp = _imageCompGameObject.GetComponent<Image>();

            if (GameUI.Instance.IsAudioEnable())
            {
                _curSpriteID = 0;
            }
            else
            {
                _curSpriteID = 1;
            }
            _imageComp.sprite = spriteList[_curSpriteID];
        }
    }
}

