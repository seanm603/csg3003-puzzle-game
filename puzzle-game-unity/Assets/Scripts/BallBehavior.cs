using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float upperBound, lowerBound, leftBound, rightBound;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    private bool _ending = false;
    private Rigidbody rb;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    void Update()
    {
        Vector3 pos = transform.position;
        if ((pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound) && !_ending)
        {
            Debug.Log("BH: Out of Bounds. Game Over.");
            EndGameSequence();
        }
    }
    void EndGameSequence()
    {
        _ending = true;
        Debug.Log("BH: EndGameSequence() called");
        rb.isKinematic = true;
        rb.useGravity = false;
        Debug.Log("BH: calling GM.SetGameOver()");
        _gameManager.SetGameOver();
    }
    void OnCollisionEnter(Collision coll)
    {
        switch (coll.gameObject.tag)
        {
            case "JumpPlate":
                _scoreManager.UpdateCurrentScore();
                rb.AddForce(Vector3.up * jumpForce);
                break;
            case "Ground":
                break;
            case "Obstacle":
                rb.isKinematic = true;
                rb.useGravity = false;
                Debug.Log("BH: calling GM.SetGameOver()");
                _gameManager.SetGameOver();
                break;
            case "Snowman":
                transform.position = new Vector3(40, 20.8f, 8.16f);
                transform.eulerAngles = Vector3.zero;
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