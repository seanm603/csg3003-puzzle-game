using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject headPrefab, snowmanPrefab, jumpPlatePrefab, groundPrefab, treePrefab2, sleighPrefab2;
    [SerializeField] private SceneLoadingManager _sceneLoadingManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private bool _isReloading;
    private GameObject head, jumpPlate, snowman, ground, tree, sleigh;
    [SerializeField] private GameObject gameOverPanel, nextLevelPanel;
    [SerializeField] private Text restartText, continueText;
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool didWin = false;
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
        if (gameOverPanel == null || nextLevelPanel == null)
        {
            Debug.Log("GM: Scene doesn't require scoring or something went wrong");
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
        string sceneName = SceneManager.GetActiveScene().name;
        if (_scoreManager != null)
        {
            _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }

        _isGameOver = false;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (nextLevelPanel != null)
        {
            nextLevelPanel.SetActive(false);
        }
        if (restartText != null)
        {
            restartText.gameObject.SetActive(false);
        }
        if (continueText != null)
        {
            continueText.gameObject.SetActive(false);
        }
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
        Debug.Log("GM: Calling SM.TryUpdateBestScore()");
        _scoreManager.TryUpdateBestScore();
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

    public void QuitGame()
    {
        Debug.Log("GM: QuitGame() called");
        SceneManager.LoadScene("TitleScene");
    }
}
