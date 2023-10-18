using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject ball, cup;
    private bool isReloading = false;

    private Vector3 ballPos, cupPos;
    void Awake()
    {
        if (isReloading)
        {
            Destroy(gameObject);
        }
    }

    void start()
    {
        ball = GameObject.Find("Ball");
        cup = GameObject.Find("Cup");
        ballPos = ball.transform.position;
        cupPos = cup.transform.position;
        if (ballPos != null && cupPos != null)
        {
            Debug.Log("Ball position: " + ballPos);
            Debug.Log("Cup position: " + cupPos);
        }
        else
        {
            Debug.Log("Ball or cup position not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
