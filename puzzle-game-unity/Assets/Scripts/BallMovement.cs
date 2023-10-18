using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1000f;
    private bool canMove = true;
    public float upperBound, lowerBound, leftBound, rightBound;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float hAxis = Input.GetAxis("Horizontal");
            Vector3 pos = transform.position;
            if (pos.x < leftBound)
            {
                pos.x = leftBound;
            }
            if (pos.x > rightBound)
            {
                pos.x = rightBound;
            }
            pos.x += hAxis * speed * Time.deltaTime;
            transform.position = pos;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canMove = false;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
        else // ball has been launched
        {
            // ball has stopped moving
            CheckBounds();
            if (GetComponent<Rigidbody>().velocity.magnitude < 0.5)
            {
                Debug.Log("Ball stopped at: " + transform.position);
            }
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
            case "TriPrism":
                Debug.Log("Hit the tri-prism!");
                break;
            case "Cup":
                Debug.Log("Hit the cup!");
                break;
            default:
                break;
        }
    }
    void CheckBounds()
    {
        Vector3 pos = transform.position;
        if (pos.x < leftBound || pos.x > rightBound || pos.y < lowerBound || pos.y > upperBound)
        {
            Debug.Log("Ball fell out of bounds!");
            SceneManager.LoadScene("GameOverScene");
        }
    }
}