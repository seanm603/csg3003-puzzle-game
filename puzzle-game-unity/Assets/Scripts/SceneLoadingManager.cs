using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    [HideInInspector] public int activeScene;
    public GameObject ballPrefab;
    public GameObject cupPrefab;
    void Awake()
    {
        ReturnActiveScene();    
    }
    void Start()
    {
        GameObject ball = Instantiate<GameObject>(ballPrefab);
        GameObject cup = Instantiate<GameObject>(cupPrefab);
    }
    public int LoadGame(int sceneIndex)
    {
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
