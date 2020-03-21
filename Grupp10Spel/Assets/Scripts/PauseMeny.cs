using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMeny : MonoBehaviour
{

    public GameObject pauseScreen = null;
    private bool isPaused = false;

    private int sceneID;

    void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;

        if(Input.GetKeyDown(KeyCode.Escape)){

            if(isPaused == true){
                Resume();
            }else{
                Pause();
            } 
        }
    }
    public void Resume(){
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
    void Pause(){
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart(){
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1f;
    }

    public void Quit(){
        Application.Quit();
    }
}
