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
    private GameObject _head, _jumpPlate, _snowman, _ground;
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
            GameObject ground = Instantiate<GameObject>(groundPrefab);
        }
        if (headPrefab != null)
        {
            GameObject head = Instantiate<GameObject>(headPrefab);
        }
        if (jumpPlatePrefab != null)
        {
            GameObject jumpPlate = Instantiate<GameObject>(jumpPlatePrefab);
        }
        if (snowmanPrefab != null)
        {
            GameObject snowman = Instantiate<GameObject>(snowmanPrefab);
        }
        if (level == "LevelTwo")
        {
            if (treePrefab2 != null)
            {
                GameObject tree = Instantiate<GameObject>(treePrefab2);
            }
            if (sleighPrefab2 != null)
            {
                GameObject sleigh = Instantiate<GameObject>(sleighPrefab2);
            }
        }

    }
    void Start()
    {
        _isGameOver = false;
        gameOverPanel.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextLevelPanel.SetActive(false);
        continueText.gameObject.SetActive(false);
        FetchComponents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            _isReloading = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToScene("TitleScene");
        }
    }

    void FetchComponents()
    {
        _sceneLoadingManager = GameObject.Find("SceneLoadingManager").GetComponent<SceneLoadingManager>();
        _ground = GameObject.Find("Ground");
        _head = GameObject.Find("Head");
        _snowman = GameObject.Find("Snowman");
        _jumpPlate = GameObject.Find("JumpPlate");


        //_head = GameObject.Find("Head");
        //Debug.Log("Found Head: " + _head.name);


        if (_sceneLoadingManager == null)
        {
            Debug.Log("Can't find SceneLoadingManager");
        }
    }

    public void Restart()
    {
        Debug.Log("Game Manager: Restart Method called");
    }
    public void GoToScene(string sceneName)
    {
        _sceneLoadingManager.LoadGame(sceneName);
    }
    // public void GoNextLevel()
    // {
    //     Debug.Log("Active Scene: " + _sceneLoadingManager.ReturnActiveScene().name);
    //     switch (_sceneLoadingManager.ReturnActiveScene().name)
    //     {
    //         case "LevelOne":
    //             _sceneLoadingManager.LoadGame("LevelTwo");
    //             break;
    //         case "LevelTwo":
    //             _sceneLoadingManager.LoadGame("Credits");
    //             break;
    //         default:
    //             Debug.Log("Next level not found -- current level -> " + _sceneLoadingManager.ReturnActiveScene().name);
    //             break;
    //     }
    // }
    public void SetWin()
    {
        Debug.Log("Game Manager: setting win");
        didWin = true;
    }
    public void SetGameOver()
    {
        Debug.Log("Game Manager: setting game over");
        _isGameOver = true;

        if (_isGameOver)
        {
            if (didWin)
            {
                SavePrefs();
                StartCoroutine(WinSequence());
                Debug.Log("Awaiting input");
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("Application Quit");
                    Application.Quit();
                }
            }
            else
            {
                StartCoroutine(GameOverSequence());
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                //If Q is hit, quit the game
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("Application Quit");
                    Application.Quit();
                }
            }

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
        Debug.Log("Saving Prefs...");
        string level = SceneManager.GetActiveScene().name;
        switch (level)
        {
            case "LevelOne":
                if (PlayerPrefs.HasKey("LevelOneScore"))
                {
                    PlayerPrefs.SetInt("LevelOneScore", Mathf.Min(PlayerPrefs.GetInt("LevelOneScore"), _score));
                }
                else
                {
                    PlayerPrefs.SetInt("LevelOneScore", _score);
                }
                break;
            case "LevelTwo":
                if (PlayerPrefs.HasKey("LevelTwoScore"))
                {
                    PlayerPrefs.SetInt("LevelTwoScore", Mathf.Min(PlayerPrefs.GetInt("LevelTwoScore"), _score));
                }
                else
                {
                    PlayerPrefs.SetInt("LevelTwoScore", _score);
                }
                break;
            default:
                Debug.Log("Level not found");
                break;
        }
        Debug.Log("Current Score: " + _score);
        Debug.Log("Current Personal Best: " + PlayerPrefs.GetInt("HighScore"));
        Debug.Log("Updated best is " + PlayerPrefs.GetInt("HighScore"));
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
        // Quit Game Logic
        Debug.LogWarning("QuitGame Not Fully Implemented. Results may vary.");
        SceneManager.LoadScene("TitleScene");

    }
}

