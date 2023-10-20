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
    private Rigidbody rb;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.isKinematic = true;
            rb.useGravity = false;
            _gameManager.SetGameOver();

        }
    }

    void OnCollisionEnter(Collision coll)
    {
        rb = GetComponent<Rigidbody>();
        switch (coll.gameObject.tag)
        {
            case "JumpPlate":
                Debug.Log("Hit the jump plate!");
                _gameManager.AddScore();
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * jumpForce);
                break;
            case "Ground":
                Debug.Log("Hit the ground!");
                break;
            case "Obstacle":
                Debug.Log("Hit obstacle. Game Over.");
                rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                _gameManager.SetGameOver();
                break;
            case "Snowman":
                Debug.Log("Snowman! You win!");
                transform.position = new Vector3(40, 20, 8.16f);
                rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                _gameManager.SetWin();
                _gameManager.SetGameOver();
                break;
            default:
                break;
        }
    }
}