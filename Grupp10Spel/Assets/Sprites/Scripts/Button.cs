using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{
    public void ExitOnClick()
    {
        Application.Quit();
    }
    public void StartOnClick()
    {
        SceneManager.LoadScene(1);
    }
}
