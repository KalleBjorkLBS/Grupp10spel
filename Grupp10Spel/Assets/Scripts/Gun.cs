using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    GameObject target = null;

    SpriteRenderer SpriteRenderer;

    public float speed = 2f;

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position; //Gets target position
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg; //Gets only 2D position and angle towards target
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward); //Points gun towards target in 2D space
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed); //How fast it can rotate

        //Changes sprite renderers y axle if the rotation is under or over 90
        if (angle > 90)
        {
            SpriteRenderer.flipY = true;
        }
        else
        {
            SpriteRenderer.flipY = false;
        }

        if (angle > -90)
        {
            SpriteRenderer.flipY = false;
        }
        else
        {
            SpriteRenderer.flipY = true;
        }
    
    }
    
}
