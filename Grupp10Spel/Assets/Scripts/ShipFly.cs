using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFly : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(-6*Time.deltaTime,0,0);    
    }
}
