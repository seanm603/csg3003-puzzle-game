using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    [HideInInspector] public int activeScene;

    void Awake()
    {
        ReturnActiveScene();

    }
    void Start()
    {
    }
    public int LoadGame(int sceneIndex)
    {
        Debug.Log("Scene Loader: Loading Scene " + sceneIndex);
        SceneManager.LoadSceneAsync(sceneIndex);
        return 1;
    }
    public string LoadGame(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        return "nice";
    }
    public Scene ReturnActiveScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        activeScene = scene.buildIndex;
        return scene;
    }
}
