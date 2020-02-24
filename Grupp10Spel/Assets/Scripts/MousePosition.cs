using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public float mouseSpeed = 1;
    float zAxis = 2f;
    Vector3 mousePosition;
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Gets mouse postion in world space
        mousePosition.z = zAxis; //Just Z axis

        transform.position = mousePosition * mouseSpeed; //How fast the object following the mouse is
    }
}
