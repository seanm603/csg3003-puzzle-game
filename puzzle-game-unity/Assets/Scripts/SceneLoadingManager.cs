using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    [HideInInspector] public int activeScene;
    public GameObject ballPrefab;
    public GameObject cupPrefab;
    public GameObject prismPrefab;
    public GameObject jumpPlatePrefab;
    void Awake()
    {
        ReturnActiveScene();    
    }
    void Start()
    {
        InstantiateGOs();
    }
    private void InstantiateGOs()
    {
        if (ballPrefab != null)
        {
            GameObject ball = Instantiate<GameObject>(ballPrefab);
        }
        if (cupPrefab != null)
        {
            GameObject cup = Instantiate<GameObject>(cupPrefab);
        }
        if (prismPrefab != null)
        {
            GameObject triPrism = Instantiate<GameObject>(prismPrefab);
        }
        if (jumpPlatePrefab != null)
        {
            GameObject jumpPlate = Instantiate<GameObject>(jumpPlatePrefab);
        }
        else
        {
            Debug.Log("jump plate fucked?");
        }
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
