using JS;
using JS.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private bool isPauseMenuShowing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (UIManager.Instance.IsUIShowing(UIName.GameOverScreen)) return;

        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        if (!isPauseMenuShowing) ShowPauseMenu(); else HidePauseMenu();
    }

    public void ShowPauseMenu()
    {
        UIManager.Instance.ShowUI(UIName.PauseGameScreen);
        var pauseMenu = UIManager.Instance.GetUI<PauseMenuPopup>(UIName.PauseGameScreen);
        pauseMenu.Init(this);
        isPauseMenuShowing = true;
        Time.timeScale = 0;
    }

    public void HidePauseMenu()
    {
        UIManager.Instance.HideUI(UIName.PauseGameScreen);
        isPauseMenuShowing = false;
        Time.timeScale = 1;
    }

}