using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private static bool _isHighScorePopupEnable = false;
    public GameObject highScorePopup;
    [SerializeField] private TextMeshProUGUI  highScoreText;

    void Start()
    {
        highScorePopup.SetActive(false);
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (_isHighScorePopupEnable)
    //         {
    //             HighScoreResume();
    //         }
    //     }
    // }

    // public void HighScoreResume()
    // {
    //     Debug.Log("High Score popup resumed");
    //     highScorePopup.SetActive(false);
    //     _isHighScorePopupEnable = false;
    // }

    public void StartGame()
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
        highScorePopup.SetActive(true);
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void CloseHighScorePopup()
    {
        Debug.Log("High Score popup closed");
        highScorePopup.SetActive(false);
    }
}
