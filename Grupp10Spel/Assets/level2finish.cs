using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2finish : MonoBehaviour
{
  
    public Transform elevatorTransform = null;
    public Collider2D elevatorCollider = null;

    private bool elevatorStart = false;

    void Update()
    {
        if(elevatorStart == true){
            elevatorTransform.position += new Vector3(0,8*Time.deltaTime,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "player"){
            elevatorStart = true;
        }
    }
}
