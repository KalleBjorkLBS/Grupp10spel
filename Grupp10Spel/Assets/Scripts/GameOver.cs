using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Image gameOverText = null;

    public Image buttonImage = null;
    public Text buttonText = null;
    

    void Update()
    {
        if(Player.isDead == true){
            gameOverText.enabled = true;
            buttonImage.enabled = true;
            buttonText.enabled = true;
        }
    }

    public void RespawnOnClick(){
        if(Player.isDead == true){
            SceneManager.LoadScene(3);
            Player.isDead = false;
        }
    }
}
