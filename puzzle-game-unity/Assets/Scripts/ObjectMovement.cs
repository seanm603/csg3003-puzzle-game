using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private Vector3 mPosition;
    private float mZCoord;
    void OnMouseDown()
    {

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        mPosition = GetMouseWorldPos();
        if (InBounds(mPosition))
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
        
    }
    bool InBounds(Vector3 mousePos)
    {
        Rect borders = new Rect(-30,2,50,75);
        if (borders.Contains(mPosition))
        {
            return true;
        } else
        {
            return false;
        }
    }
}