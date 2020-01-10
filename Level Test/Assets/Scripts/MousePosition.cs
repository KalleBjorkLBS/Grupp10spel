using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public float mouseSpeed = 0.5f;
    float zAxis = 2f;
    Vector3 mousePosition;
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = zAxis;

        transform.position = mousePosition;
    }
}
