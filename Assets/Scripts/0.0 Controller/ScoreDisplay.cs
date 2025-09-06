using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isShowBestScore = false;
    private void OnEnable()
    {
        if (isShowBestScore)
        {
            int score = PlayerPrefs.GetInt("HighScore", 0);
            scoreText.text = score.ToString("D8");
        }
    }
    private void Update()
    {
        if (ScoreManager.Instance == null) return;

        if (!isShowBestScore)
        {
            int score = ScoreManager.Instance.GetScore();
            scoreText.text = score.ToString("D8");
        }
    }
}