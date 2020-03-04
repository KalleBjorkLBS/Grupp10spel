using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Image gameOverText = null;
    
    void Update()
    {
        if(Player.isDead == true){
            gameOverText.enabled = true;
        }    
    }
}
