using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    public float xBound, upBound, lowBound, moveSpeed;
    private Vector3 mOffset;
    private Vector3 mPosition;
    private float mZCoord;
    private SceneLoadingManager _sceneLoadingManager;
    private GameManager _gameManager;

    void Start()
    {
        _sceneLoadingManager = GameObject.Find("SceneLoadingManager").GetComponent<SceneLoadingManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        MoveObject();
    }
    void MoveObject()
    {
        float tilt = Input.GetAxis("Horizontal");
        // // float vAxis = Input.GetAxis("Vertical");
        // float hAxis = Input.GetAxis("Mouse X");
        // float vAxis = Input.GetAxis("Mouse Y");
        // // float lTilt = Input.GetAxis("Mouse Y");
        // Vector3 pos = transform.position;
        // if (pos.x < -xBound)
        // {
        //     pos.x = -xBound;
        // }
        // if (pos.x > xBound)
        // {
        //     pos.x = xBound;
        // }
        // if (pos.y < lowBound)
        // {
        //     pos.y = lowBound;
        // }
        // if (pos.y > upBound)
        // {
        //     pos.y = upBound;
        // }

        // pos.x += hAxis * moveSpeed * Time.deltaTime;
        // pos.y += vAxis * moveSpeed * Time.deltaTime;
        // transform.position = pos;
        Vector3 mousePos2D = Input.mousePosition;

        // instructs how far to push mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // convert point from 2D screen to 3D world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        pos.y = mousePos3D.y;
        transform.position = pos;
        transform.Rotate(0, 0, -tilt * moveSpeed * Time.deltaTime);
    }
}