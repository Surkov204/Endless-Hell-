using JS.Utils;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScoreType
{
    Easy,
    Normal,
    Hard
}

public class ScoreManager : ManualSingletonMono<ScoreManager>
{
    private int currentScore;
    private float timer;
    private bool isAlive = true;
    [Header("Score Settings")]
    [SerializeField] private int timeScoreRate = 1;
    [SerializeField] private float timeInterval = 0.2f;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame_1")
        {
            ResetScore();
        }
    }

    private void Awake()
    {
        base.Awake();
        if (Instance != this) return;
    }
    private void Update()
    {
        if (!isAlive) return;
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            currentScore += timeScoreRate;
            timer = 0f;
        }
    }

    public void ScoreApply(ScoreType type)
    {
        if (!isAlive) return;

        int value = 0;
        switch (type)
        {
            case ScoreType.Easy: value = 10; break;
            case ScoreType.Normal: value = 100; break;
            case ScoreType.Hard: value = 1000; break;
        }

        currentScore += value;
    }

    public int GetScore() => currentScore;

    public void SaveScore()
    {
        int best = PlayerPrefs.GetInt("HighScore", 0);
        int finalBest = Mathf.Max(currentScore, best);
        PlayerPrefs.SetInt("HighScore", finalBest);
        PlayerPrefs.Save();
        Debug.Log($"[ScoreManager] HighScore saved = {finalBest}");
    }

    public int LoadHighScore()
    {
       return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetScore()
    {
        isAlive = true;
        currentScore = 0;
        Debug.Log("[ScoreManager] Score reset = 0");
    }

    public void StopAplyScore(){
        SaveScore();
        isAlive = false;
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
