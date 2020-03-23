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
        startpos = transform.position.x; //finds the starting point of the sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x; //finds width of sprite
        
    }

   
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); 
        float dist = (cam.transform.position.x * parallaxEffect); //calculates how much the sprite should move relative to the camera times the parallax effect

        transform.position = new Vector3(startpos + dist, transform.position.y , transform.position.z); //moves the sprite on the x axis relative to the camera position times the parallax effect

        if (temp > startpos + length) startpos += length; //moves the sprite the same distance as its length in the direction it is moving when it has moved more than it's distance so it can "repeat"(right)
        else if (temp < startpos - length) startpos -= length; //moves the sprite the same distance as its length in the direction it is moving when it has moved more than it's distance so it can "repeat"(left)
    }
}
