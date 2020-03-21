using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpos = transform.position.x; //Hittar startpositionen på sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x; //Hittar storleken på spriten
        
    }

   
    void Update() //Flyttar på spriten relativt till camerapositionen och flyttar spriten till mitten av kameran när den har rört sig mer än dess längd 
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y , transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
