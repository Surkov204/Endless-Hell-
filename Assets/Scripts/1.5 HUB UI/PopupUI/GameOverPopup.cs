using JS;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : UIBase
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        base.Awake();
        restartButton.onClick?.AddListener(RestartButton);
        mainMenuButton.onClick?.AddListener(BackToMainMenu);
    }

    private void RestartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ScoreManager.Instance.ResetScore();
    }

    private void BackToMainMenu() {
        SceneLoader.Load("MainMenu");
    }
}
