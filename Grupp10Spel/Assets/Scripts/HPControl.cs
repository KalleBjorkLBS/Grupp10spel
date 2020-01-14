using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPControl : MonoBehaviour
{

    [SerializeField]
    Image health1 = null;
    [SerializeField]
    Image health2 = null;
    [SerializeField]
    Image health3 = null;

    void Update()
    {

        //print(Player.healthLeft);

        if(Player.healthLeft == 2)
        {
            health3.enabled = false;
        }

        if(Player.healthLeft == 1)
        {
            health2.enabled = false;
        }

        if(Player.healthLeft == 0)
        {
            health1.enabled = false;
        }
    }
}
