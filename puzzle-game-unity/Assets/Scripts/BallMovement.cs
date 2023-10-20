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
    }
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
            _gameManager.SetGameOver();
            _gameManager.Restart();
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
                break;
            default:
                break;
        }
    }
}