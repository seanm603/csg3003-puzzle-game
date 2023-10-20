using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    [HideInInspector] public int activeScene;
    public GameObject headPrefab, snowmanPrefab, jumpPlatePrefab, groundPrefab;
    void Awake()
    {
        ReturnActiveScene();
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
