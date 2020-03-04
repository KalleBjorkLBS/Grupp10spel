using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightButton : MonoBehaviour
{

    public GameObject door = null;
    public static bool playerHit = false;
    private bool buttonHit = false;

    private ParticleSystem partSystem;

    void Awake(){
        partSystem = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(buttonHit == true){
            door.transform.position += new Vector3(0,0.1f,0);
        }

        if(Player.isDead == true){
            partSystem.Pause();
        }
    }

    private void OnParticleCollision(GameObject other) {
        if(other.tag == "bossButton"){
           buttonHit = true;
        } 

        if(other.tag == "player"){
            playerHit = true;
        }              
               
    }
}
