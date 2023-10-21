using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject headPrefab, snowmanPrefab, jumpPlatePrefab, groundPrefab, treePrefab2, sleighPrefab2;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private bool _isReloading;
    private GameObject head, jumpPlate, snowman, ground, tree, sleigh;
    [SerializeField] private GameObject gameOverPanel, nextLevelPanel;
    [SerializeField] private Text restartText, continueText;
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool didWin = false;
    private string sceneName;
    // Start is called before the first frame update
    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("GM: Waking " + sceneName);

        // Instantiate GameObjects for Scene
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
        if (sceneName == "LevelTwo")
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
        Debug.Log("GM: Starting " + sceneName);
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
        // Enables in-game reload and quit mechanism
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
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("GM: Active Scene - " + sceneName);
                    Debug.Log("GM: Loading Scene " + (SceneManager.GetActiveScene().buildIndex + 1));
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    QuitGame();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                }

                //If Q is hit, quit the game
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    QuitGame();
                }
            }

        }
    }

    public void Restart()
    {
        Debug.Log("GM: Restarting " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    public void SetWin()
    {
        Debug.Log("GM: SetWin() called");
        didWin = true;
    }
    public void SetGameOver()
    {
        Debug.Log("GM: SetGameOver() called");
        _isGameOver = true;
        if (didWin)
        {
            Debug.Log("GM: Calling SM.TryUpdateBestScore()");
            _scoreManager.TryUpdateBestScore();
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

        yield return new WaitForSeconds(3.0f);

        restartText.gameObject.SetActive(true);
    }
    private IEnumerator WinSequence()
    {
        nextLevelPanel.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        continueText.gameObject.SetActive(true);
    }

    public void GoToScene(string sceneName)
    {
        // Used to implement buttons in the game
        // Each button uses this method on click
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("GM: QuitGame() called");
        SceneManager.LoadScene("TitleScene");
    }
}
