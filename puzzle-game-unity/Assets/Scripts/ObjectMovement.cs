using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float xBound, yBound, width, height;
    private Vector3 mOffset;
    private Vector3 mPosition;
    private float mZCoord;
    private bool _isSelected = false;
    void Update()
    {
        if (_isSelected)
        {
            Vector3 mousePos2D = Input.mousePosition;

            // instructs how far to push mouse into 3D
            //mousePos2D.z = -Camera.main.transform.position.z;

            // convert point from 2D screen to 3D world
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

            Vector3 pos = mousePos3D;
            //pos.x = mousePos3D.x;
            this.transform.position = pos;
            if (Input.GetMouseButtonUp(0))
            {
                _isSelected = false;
            }
        }
        else
        {
            return;
        }
        
    }
    void OnMouseDown()
    {
        //Debug.Log("Mouse down");
        //mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // store offset = gameobject world pos - mouse world pos
        //mOffset = gameObject.transform.position - GetMouseWorldPos();
        if(!_isSelected)
        {
            Debug.Log("Selected: " + this.name);
            _isSelected = true;
        } else
        {
            Debug.Log("Un-selected: " + this.name);
            _isSelected = false;
        }
    }
    void OnMouseEnter()
    {
        //Debug.Log("Mouse enters");
    }
    void OnMouseDrag()
    {
        
        //Debug.Log("Mouse drags");
        //mPosition = GetMouseWorldPos();
        //if (InBounds(mPosition))
        //{
            //transform.position = GetMouseWorldPos() + mOffset;
        //}
        
    }
    void OnMouseExit()
    {
        //Debug.Log("Mouse exits");    
    }
    void OnMouseOver()
    {
        //Debug.Log("Mouse over");
        
    }
    void OnMouseUp()
    {
        
    }
    void OnMouseUpAsButton()
    {
        
    }
    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private bool InBounds(Vector3 mousePos)
    {
        Rect borders = new Rect(xBound, yBound, width, height);
        if (borders.Contains(mPosition))
        {
            return true;
        } else
        {
            return false;
        }
    }
}