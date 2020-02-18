using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    
    [SerializeField]
    GameObject target = null;

    private float laserCooldown = 0f;
    private float laserShotTimer = 0f;

    Vector2 targetFixed;
    
    void FixedUpdate()
    {   
        laserCooldown += 1f*Time.deltaTime;

        if(laserCooldown <= 3){
            targetFixed = target.transform.position + new Vector3(target.transform.position.x, target.transform.position.y - 15, target.transform.position.y);
        } else if( laserCooldown >= 3){
            laserShotTimer += 1f*Time.deltaTime;
        }

        if(laserShotTimer >= 1f){
            laserCooldown = 0;
            laserShotTimer = 0;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position, 100f);
        Debug.DrawRay(transform.position, targetFixed, Color.magenta, 3f); 



        if(hit.collider.tag == "player"){
            //TODO Kill and kill anim
        }  
    }


}
