using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoadingManager _sceneLoadingManager;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private bool _isReloading;
    private GameObject _head;
    private GameObject _snowman;
    private GameObject _ground;
    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
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
        //_head = GameObject.Find("Head");
        //Debug.Log("Found Head: " + _head.name);


        if (_sceneLoadingManager == null)
        {
            Debug.Log("Can't find SceneLoadingManager");
        }
    }

    public void Restart(int sceneID)
    {
        Debug.Log("Game Manager: Restarting...");
        Debug.Log("Loading Scene: " + sceneID);
        _sceneLoadingManager.LoadGame(sceneID);
    }
    public void GoToScene(string sceneName)
    {
        _sceneLoadingManager.LoadGame(sceneName);
    }
    public void GoNextLevel()
    {
        Debug.Log("Active Scene: " + _sceneLoadingManager.ReturnActiveScene().name);
        switch (_sceneLoadingManager.ReturnActiveScene().name)
        {
            case "LevelOne":
                _sceneLoadingManager.LoadGame("LevelTwo");
                break;
            case "LevelTwo":
                _sceneLoadingManager.LoadGame("Credits");
                break;
            default:
                Debug.Log("Next level not found -- current level -> " + _sceneLoadingManager.ReturnActiveScene().name);
                break;
        }
    }
    public void SetGameOver()
    {
        _isGameOver = true;

    }
    public void QuitGame()
    {
        // Quit Game Logic
        Debug.LogWarning("QuitGame Not Fully Implemented. Results may vary.");
        SceneManager.LoadScene("TitleScene");

    }
}
