using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject highScoreScreen;
    [SerializeField] private TextMeshProUGUI  highScoreText;

    void Start()
    {
        highScoreScreen.SetActive(false);
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

    public void OpenHighScoreScreen()
    {
        Debug.Log("High Score Screen opened");
        highScoreScreen.SetActive(true);
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void CloseHighScoreScreen()
    {
        Debug.Log("High Score Screen closed");
        highScoreScreen.SetActive(false);
    }
}
