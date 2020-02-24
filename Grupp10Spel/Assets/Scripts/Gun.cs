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
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

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
