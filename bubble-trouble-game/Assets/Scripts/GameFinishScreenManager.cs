using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject gameFinishScreen;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private TextMeshProUGUI  pointsText;
    [SerializeField] private Button mainMenuButton;
    
    private const string WinText = "You Won The Game!";
    private const string LoseText = "Game over!";
    private void Start()
    {
        gameFinishScreen.SetActive(false);
        mainMenuButton.onClick.AddListener(GoToMainMenu); 
    }
    
    public void Setup(bool win, int score, bool highScore)
    {
        // Show end game panel
        // win/lose text + the player score + high score if it's highest + go to main menu button
        gameFinishScreen.SetActive(true);
        gameStatusText.text = win ? WinText : LoseText;
        pointsText.text = score.ToString() + " Points";
        if (highScore)
        {
            pointsText.text += "\nHigh Score!";
        }
        // Pause the game
        Time.timeScale = 0f;
    }

    private void GoToMainMenu()
    {
        // Resume game pause and go to main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(GoToMainMenu);
    }
}
