using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int speed = 10;

    // Update is called once per frame
    void Update()
    {
        // current mouse position
        // Vector3 mousePos2D = Input.mousePosition;

        // // instructs how far to push mouse into 3D
        // mousePos2D.z = -Camera.main.transform.position.z;

        // // convert point from 2D screen to 3D world
        // Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Vector3 pos = this.transform.position;
        // pos.x = mousePos3D.x;
        // this.transform.position = pos;

        float hAxis = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        transform.position = pos;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

        }

    }
}
