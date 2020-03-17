using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    [SerializeField]
    GameObject hiss = null;

    
    void Update()
    {
        if(hiss.transform.position.y <-11){
            hiss.transform.position += new Vector3(0,5*Time.deltaTime,0);
        }
    }
}
