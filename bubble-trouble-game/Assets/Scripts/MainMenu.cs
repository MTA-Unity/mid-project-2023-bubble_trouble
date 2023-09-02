using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject highScorePopup;
    [SerializeField] private TextMeshProUGUI  highScoreText;

    void Start()
    {
        highScorePopup.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log("Load Level1");
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        // Exit the game in in editor and in application
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
