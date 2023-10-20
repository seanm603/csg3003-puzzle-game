using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject headPrefab, snowmanPrefab, jumpPlatePrefab, groundPrefab, treePrefab2, sleighPrefab2;
    public ScoreCounter scoreCounter;
    [SerializeField] private SceneLoadingManager _sceneLoadingManager;
    [SerializeField] private bool _isReloading;
    private GameObject head, jumpPlate, snowman, ground, tree, sleigh;
    [SerializeField] private GameObject gameOverPanel, nextLevelPanel;
    [SerializeField] private Text restartText, continueText;
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool didWin = false;
    private int _score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        string level = SceneManager.GetActiveScene().name;
        if (groundPrefab != null)
        {
            ground = Instantiate<GameObject>(groundPrefab);
        }
        if (headPrefab != null)
        {
            head = Instantiate<GameObject>(headPrefab);
        }
        if (jumpPlatePrefab != null)
        {
            jumpPlate = Instantiate<GameObject>(jumpPlatePrefab);
        }
        if (snowmanPrefab != null)
        {
            snowman = Instantiate<GameObject>(snowmanPrefab);
        }
        if (level == "LevelTwo")
        {
            if (treePrefab2 != null)
            {
                tree = Instantiate<GameObject>(treePrefab2);
            }
            if (sleighPrefab2 != null)
            {
                sleigh = Instantiate<GameObject>(sleighPrefab2);
            }
        }

    }
    void Start()
    {
        Debug.Log("GM: Starting " + SceneManager.GetActiveScene().name);
        Debug.Log("Current High Score: " + PlayerPrefs.GetInt("LevelOneScore"));
        _isGameOver = false;
        gameOverPanel.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextLevelPanel.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            _isReloading = true;
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if (_isGameOver)
        {
            if (didWin)
            {
                Debug.Log("GM: Quitting Game - Win Case");
                Debug.Log("GM: Awaiting input");
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("GM: Enter pressed");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("GM: Q pressed");
                    QuitGame();
                    Application.Quit();
                }
            }
            else
            {
                Debug.Log("GM: Quitting Game - Game Over Case");
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("GM: R pressed");
                    Restart();
                }

                //If Q is hit, quit the game
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("GM: Q pressed");
                    QuitGame();
                }
            }

        }
    }

    public void Restart()
    {
        Debug.Log("GM: Restart() called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToScene(string sceneName)
    {
        _sceneLoadingManager.LoadGame(sceneName);
    }
    public void SetWin()
    {
        Debug.Log("GM: setting win");
        didWin = true;
    }
    public void SetGameOver()
    {
        Debug.Log("GM: Setting game over");
        _isGameOver = true;
        Debug.Log("GM: Saving PlayerPrefs");
        SavePrefs();
        Debug.Log("GM: Loading EndScene");
        if (didWin)
        {
            StartCoroutine(WinSequence());
        }
        else
        {
            StartCoroutine(GameOverSequence());
        }
    }
    private IEnumerator GameOverSequence()
    {
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        restartText.gameObject.SetActive(true);
    }
    private IEnumerator WinSequence()
    {
        Debug.Log("Starting WinSequence Coroutine");
        nextLevelPanel.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        continueText.gameObject.SetActive(true);
    }

    public void SavePrefs()
    {
        string level = SceneManager.GetActiveScene().name;
        Debug.Log("GM: Saving PlayerPrefs for " + level);
        switch (level)
        {
            case "LevelOne":
                string scoreKey = "LevelOneScore";
                if (PlayerPrefs.HasKey(scoreKey))
                {
                    Debug.Log("GM: Prior score found, comparing " + PlayerPrefs.GetInt(scoreKey) + " to " + _score);
                    PlayerPrefs.SetInt("LevelOneScore", Mathf.Min(PlayerPrefs.GetInt(scoreKey), _score));
                }
                else
                {
                    Debug.Log("GM: No prior score found, setting new score");
                    PlayerPrefs.SetInt("LevelOneScore", _score);
                }
                break;
            case "LevelTwo":
                scoreKey = "LevelTwoScore";
                if (PlayerPrefs.HasKey(scoreKey))
                {
                    Debug.Log("GM: Prior score found, comparing " + PlayerPrefs.GetInt(scoreKey) + " to " + _score);
                    PlayerPrefs.SetInt("LevelTwoScore", Mathf.Min(PlayerPrefs.GetInt(scoreKey), _score));
                }
                else
                {
                    Debug.Log("GM: No prior score found");
                    PlayerPrefs.SetInt("LevelTwoScore", _score);
                }
                break;
            default:
                Debug.Log("Level not found");
                break;
        }
    }
    public void AddScore()
    {
        _score++;
    }
    public int GetScore()
    {
        return _score;
    }

    public void QuitGame()
    {
        Debug.Log("GM: QuitGame() called");
        SceneManager.LoadScene("TitleScene");
    }
}
