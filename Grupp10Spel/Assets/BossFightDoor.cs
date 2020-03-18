using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightDoor : MonoBehaviour
{   
    public Text eToOpen = null;
    Rigidbody2D rb2D = null;
    public static bool openDoor = false;

    public static bool doorReady = false;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(openDoor == true){
            rb2D.position += new Vector2(0,5*Time.deltaTime);
          
        }

        if(rb2D.position.y > 15){
            doorReady = false;
            openDoor = false;
            Player.hasControl = true;
        }else{
            doorReady = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
           if(other.gameObject.tag =="player"){
               eToOpen.text ="Press E to Open";
           }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "player"){
            if(Input.GetKeyDown(KeyCode.E)){
                openDoor = true;
                Player.hasControl = false;
                eToOpen.text ="";
            }
        }
    }
}
