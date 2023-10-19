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
            Application.Quit();
        }
    }

    void FetchComponents()
    {
        _sceneLoadingManager = GameObject.Find("SceneLoadingManager").GetComponent<SceneLoadingManager>();


        if (_sceneLoadingManager == null)
        {
            Debug.Log("Can't find SceneLoadingManager");
        }
    }

    public void Restart()
    {
        Debug.LogWarning("Restart Logic Not Implemented");
        
        if (_isGameOver)
        {
            int sceneID = _sceneLoadingManager.activeScene;
            _sceneLoadingManager.LoadGame(sceneID);
        }
        else
        {
            Debug.Log("You can not restart now...");
        }
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
        Debug.LogWarning("QuitGame Not Implemented");

    }
}
