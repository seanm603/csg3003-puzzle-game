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
                    SCORE = PlayerPrefs.GetInt("LevelOneScore");
                    Debug.Log("HighScore: current stored score is " + _gameManager.GetScore());
                }
                PlayerPrefs.SetInt("LevelOneScore", SCORE);
                if (_UI_TEXT != null)
                {
                    _UI_TEXT.text = "Personal Best: " + SCORE.ToString("#,0");
                }
                break;
            case "LevelTwo":
                if (PlayerPrefs.HasKey("LevelTwoScore"))
                {
                    SCORE = PlayerPrefs.GetInt("LevelTwoScore");
                    Debug.Log("HighScore: current stored score is " + _gameManager.GetScore());
                }
                PlayerPrefs.SetInt("LevelTwoScore", SCORE);
                if (_UI_TEXT != null)
                {
                    _UI_TEXT.text = "Personal Best: " + SCORE.ToString("#,0");
                }
                break;
        }

    }

    static public int SCORE
    {
        get { return (_SCORE); }
        private set
        {
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", value);
            if (_UI_TEXT != null)
            {
                _UI_TEXT.text = "Personal Best: " + value.ToString("#,0");
            }
        }
    }

    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 100);
            Debug.LogWarning("PlayerPrefs HighScore reset to 100.");
        }
    }

}
