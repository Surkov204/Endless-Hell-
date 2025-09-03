using JS;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuPopup : UIBase
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private string gameplaySceneName = "";
    [SerializeField] private string SelectLevelSceneName = "SelectMap";

    private UIController controller;

    public void Init(UIController ctrl)
    {
        controller = ctrl;
    }

    protected override void Awake()
    {
        base.Awake();

        continueButton.onClick.AddListener(OnContinueClick);
        replayButton.onClick.AddListener(OnReplayClick);
        quitButton.onClick.AddListener(OnQuitClick);
    }

    public void OnContinueClick()
    {
        controller.HidePauseMenu();
    }


    public void OnReplayClick()
    {
        UIManager.Instance.HideAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OnQuitClick()
    {
    }
}
