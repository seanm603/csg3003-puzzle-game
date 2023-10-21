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

    void Start()
    {
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
        Debug.Log("GM: Updating scoring UI");
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            Debug.Log("GM: Updating scoring UI for LevelOne");
            bestScoreText.text = "PR: " + bestScoreL1.ToString("#,0");
            currentScoreText.text = GetCurrentScore().ToString("#,0");
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            Debug.Log("GM: Updating scoring UI for LevelTwo");
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
        Debug.Log("SM: Updating current score");
        _score++;
    }
    public void TryUpdateBestScore()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            if (_score < bestScoreL1)
            {
                Debug.Log("SM: evaluating " + _score + " < " + bestScoreL1 + " for LevelOne");
                PlayerPrefs.SetInt("BestScoreL1", _score);
                bestScoreL1 = _score;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            if (_score < bestScoreL2)
            {
                Debug.Log("SM: evaluating " + _score + " < " + bestScoreL2 + " for LevelTwo");
                PlayerPrefs.SetInt("BestScoreL2", _score);
                bestScoreL2 = _score;
            }
        }
    }
    public int GetCurrentScore()
    {
        Debug.Log("SM: Returning score");
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

