using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float upperBound, lowerBound, leftBound, rightBound;
    private SceneLoadingManager _sceneLoadingManager;
    private GameManager _gameManager;

    // Update is called once per frame
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        _sceneLoadingManager = GameObject.Find("SceneLoadingManager").GetComponent<SceneLoadingManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("Found: " + _sceneLoadingManager + " and " + _gameManager);
        Debug.Log("Scene loading manager active scene: " + _sceneLoadingManager.activeScene);
    }
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound)
        {
            Debug.Log("Setting game over");
            _gameManager.SetGameOver();
            Debug.Log("Setting Game Over Scene");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        switch (coll.gameObject.tag)
        {
            case "JumpPlate":
                Debug.Log("Hit the jump plate!");
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * jumpForce);
                break;
            case "Ground":
                Debug.Log("Hit the ground!");
                break;
            case "Snowman":
                Debug.Log("Snowman! You win!");
                _sceneLoadingManager.LoadGame("LevelPassed");
                break;
            default:
                break;
        }
    }
}