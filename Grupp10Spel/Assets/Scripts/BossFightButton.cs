using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightButton : MonoBehaviour
{

    public GameObject door = null;
    public static bool playerHit = false;
    private bool buttonHit = false;
    void Update()
    {
        if(buttonHit == true){
            door.transform.position += new Vector3(0,0.1f,0);
        }
    }

    private void OnParticleCollision(GameObject other) {
        if(other.tag == "bossButton"){
           buttonHit = true;
        } 

        if(other.tag == "player"){
            print("player");
            playerHit = true;
        }              
               
    }
}
