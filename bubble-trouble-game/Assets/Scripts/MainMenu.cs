using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool isHighScorePopupEnable = false;
    public GameObject HighScorePopup;


    public void Awake()
    {

    }


    void Start()
    {
        HighScorePopup.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isHighScorePopupEnable)
            {
                HighScoreResume();
            }
        }
    }

    public void HighScoreResume()
    {
        Debug.Log("High Score popup resumed");
        HighScorePopup.SetActive(false);
        isHighScorePopupEnable = false;

    }


    public void startGame()
    {
        Debug.Log("Load Level1");
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Debug.Log("Game closed");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OpenHighScorePopup()
    {
        Debug.Log("High Score popup opened");
        HighScorePopup.SetActive(true);
        isHighScorePopupEnable = true;
    }
}
