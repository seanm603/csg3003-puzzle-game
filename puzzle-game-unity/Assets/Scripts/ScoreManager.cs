using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int _score = 0;
    [SerializeField] private int bestScoreL1 = 999;
    [SerializeField] private int bestScoreL2 = 999;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text currentScoreText;
    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        bestScoreL1 = PlayerPrefs.GetInt("BestScoreL1", 999);
        bestScoreL2 = PlayerPrefs.GetInt("BestScoreL2", 999);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoringUI();
    }

    private void UpdateScoringUI()
    {
        if (sceneName.Equals("LevelOne"))
        {
            bestScoreText.text = "PR: " + bestScoreL1.ToString("#,0");
            currentScoreText.text = GetCurrentScore().ToString("#,0");
        }
        else if (sceneName.Equals("LevelTwo"))
        {
            bestScoreText.text = bestScoreL2.ToString("#,0");
            currentScoreText.text = _score.ToString("#,0");
        }
    }
    public void ResetBestScore(string level)
    {
        if (level.Equals("LevelOne"))
        {
            PlayerPrefs.SetInt("BestScoreL1", 999);
            bestScoreL1 = 999;
        }
        else if (level.Equals("LevelTwo"))
        {
            PlayerPrefs.SetInt("BestScoreL2", 999);
            bestScoreL2 = 999;
        }
        else if (level.Equals("Both"))
        {
            PlayerPrefs.SetInt("BestScoreL1", 999);
            PlayerPrefs.SetInt("BestScoreL2", 999);
            bestScoreL1 = 999;
            bestScoreL2 = 999;
        }
    }
    public void UpdateCurrentScore()
    {
        _score++;
    }
    public void TryUpdateBestScore()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            if (_score < bestScoreL1)
            {
                PlayerPrefs.SetInt("BestScoreL1", _score);
                bestScoreL1 = _score;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            if (_score < bestScoreL2)
            {
                PlayerPrefs.SetInt("BestScoreL2", _score);
                bestScoreL2 = _score;
            }
        }
    }
    public int GetCurrentScore()
    {
        return _score;
    }
    public int GetBestScore(string level)
    {
        if (level.Equals("LevelOne"))
        {
            return bestScoreL1;
        }
        else if (level.Equals("LevelTwo"))
        {
            return bestScoreL2;
        }
        else
        {
            return 999;
        }
    }
}

