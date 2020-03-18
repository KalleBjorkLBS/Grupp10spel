using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight1 : MonoBehaviour
{
    
    [SerializeField]
    GameObject target = null;

    [SerializeField]
    ParticleSystem gunEffect = null;

    [SerializeField]
    Transform laserTransform = null;

    private float laserCooldown = 0f;
    private float laserShotTimer = 0f;

    private bool targetSaved = false;

    Vector3 targetFixed;
    
    void FixedUpdate()
    {   
        if(BossFightDoor.doorReady == false)
        {
        laserCooldown += 1f*Time.deltaTime;

        if(laserCooldown < 0.5f && targetSaved == false){
            TargetMethod();
        } else if( laserCooldown > 0.5f){
            laserShotTimer += 1f*Time.deltaTime;
        }

        if(laserShotTimer > 1f){
            laserCooldown = 0;
            laserShotTimer = 0;
            targetSaved = false;
        }

        if(laserShotTimer > 0){
            gunEffect.Play();
           
        } else{
            gunEffect.Stop();
            gunEffect.Clear();
        } 

        //Same as Gun.cs
        float speed = 360f;
        Vector3 vectorToTarget = targetFixed - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        laserTransform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }
    }

    void TargetMethod(){
        targetFixed = target.transform.position + new Vector3(target.transform.position.x, target.transform.position.y - 15, target.transform.position.y);
        targetSaved = true;
    }

}
