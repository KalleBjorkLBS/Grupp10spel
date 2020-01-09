using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb = null;
    [SerializeField]
    GameObject gunObject = null;

    [Range(0.0f, 10f)]
    public float fallMultiplier;
    [Range(0.0f, 10f)]
    public float lowJumpMultiplier;
    [Range(0.0f, 100f)]
    public float jumpMultiplier;
    [Range(0.0f,20f)]
    public float shotPower;

    private bool isFlying = false;
    private bool isGrounded = false;
    private float reloadTime = 1f;
    private int shotsLeft = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0, 10 * jumpMultiplier));
            isGrounded = false;
        }

        if (rb.velocity.y <0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        #region SHOOOOT GUN

        if (Input.GetMouseButtonDown(0) && shotsLeft > 0)
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            isGrounded = false;
            isFlying = true;

            reloadTime = 0;
            shotsLeft -= 1;
        }

        if(Input.GetMouseButtonDown(0) && shotsLeft >= 0 && isFlying == true)
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            shotsLeft -= 1;
        }
        
        if (isGrounded == true)
        {
            reloadTime += 1.1f * Time.deltaTime;

            rb.SetRotation(0);
        }

        if (reloadTime > 1 && isGrounded == true)
        {
            shotsLeft = 2;
        }

        #endregion

        #region Move

        if (Input.GetKey(KeyCode.D) && isGrounded == true)
        {
            rb.AddForce(new Vector2(5, 0));
        }

        if (Input.GetKey(KeyCode.A) && isGrounded == true)
        {
            rb.AddForce(new Vector2(-5, 0));
        }
        #endregion


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "mark")
        {
            isGrounded = true;
        }

        if(collision.gameObject.tag == "enemy")
        {
            
        }
    }


}
