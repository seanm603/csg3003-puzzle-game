using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlateBehavior : MonoBehaviour
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
        Vector3 mousePos2D = Input.mousePosition;

        // instructs how far to push mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // convert point from 2D screen to 3D world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        pos.y = mousePos3D.y;
        transform.position = pos;
        transform.Rotate(0, 0, -tilt * moveSpeed * Time.deltaTime * 2);
    }
}