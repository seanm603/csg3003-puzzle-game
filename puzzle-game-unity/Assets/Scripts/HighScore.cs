using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    public GameManager _gameManager;
    static private Text _UI_TEXT;
    static private int _SCORE = 100;

    private Text txtCom; // reference to this GO's Text component

    void Awake()
    {
        _UI_TEXT = GetComponent<Text>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        string level = SceneManager.GetActiveScene().name;
        switch (level)
        {
            case "LevelOne":
                if (PlayerPrefs.HasKey("LevelOneScore"))
                {
                    _SCORE = PlayerPrefs.GetInt("LevelOneScore");
                    Debug.Log("HS: current stored score is " + _SCORE);
                }
                PlayerPrefs.SetInt("LevelOneScore", _SCORE);
                if (_UI_TEXT != null)
                {
                    _UI_TEXT.text = "Personal Best: " + _SCORE.ToString("#,0");
                }
                break;
            case "LevelTwo":
                if (PlayerPrefs.HasKey("LevelTwoScore"))
                {
                    _SCORE = PlayerPrefs.GetInt("LevelTwoScore");
                    Debug.Log("HighScore: current stored score is " + _SCORE);
                }
                PlayerPrefs.SetInt("LevelTwoScore", _SCORE);
                if (_UI_TEXT != null)
                {
                    _UI_TEXT.text = "Personal Best: " + _SCORE.ToString("#,0");
                }
                break;
        }

    }

    [Tooltip("Checking this box resets best scores fro ALL levels")]
    public bool resetHighScoreNow = false;

    // void OnDrawGizmos()
    // {
    //     string level = SceneManager.GetActiveScene().name;
    //     if (resetScoresNow)
    //     {
    //         Debug.Log("HS: Resetting High Score to 100 now");
    //         resetScoresNow = false;
    //         PlayerPrefs.SetInt("LevelOneScore", 999);
    //         PlayerPrefs.SetInt("LevelTwoScore", 999);
    //     }
    // }
    void OnDrawGizmos()
    {
        Debug.Log("Reset!");
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("LevelOneScore", 1000);
            PlayerPrefs.SetInt("LevelTwoScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
        }
    }
}
