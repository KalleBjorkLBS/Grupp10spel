﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject gunObject = null;
    Rigidbody2D rb = null;
    Animator animator;
    [SerializeField]
    ParticleSystem gunShots = null;
    [SerializeField]
    Camera cam = null;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 100f;
    public float shotPower = 10f;
    private float reloadTime;
    private float flyTimer = 0;
    
    private bool isFlying = false;
    private bool isGrounded = false;
   
    public static int healthLeft = 3;
    private int shotsLeft = 2;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        cam.transform.position = transform.position + (new Vector3(0,0,-10));

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        //Control (KEEP OUT)

        #region Enkel walk + jump

        if (Input.GetKey(KeyCode.D) && isGrounded == true)
        {
            rb.velocity = new Vector2(3, 0);
        } else if (Input.GetKey(KeyCode.A) && isGrounded == false)
        {
            rb.AddForce(new Vector2(20, 0));
        }

        if (Input.GetKey(KeyCode.A) && isGrounded == true)
        {
            rb.velocity = new Vector2(-3, 0);
        } else if (Input.GetKey(KeyCode.A) && isGrounded == false)
        {
            rb.AddForce(new Vector2(-20, 0));
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded == true)
        {
            animator.SetFloat("WalkingAnim", 1);
        }
        else
        {
            animator.SetFloat("WalkingAnim", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0, 10 * jumpMultiplier));
            isGrounded = false;
        } 
        
        #endregion

        #region SHOOOOT GUN

        if (Input.GetMouseButtonDown(0) && shotsLeft == 2)
        {
            GunMethod(1);

            animator.SetBool("FlyingAnim", isFlying);
            gunShots.Play();

            reloadTime = 0;
            
        }

        if (Input.GetMouseButtonDown(0) && shotsLeft == 1 && isFlying == true)
        {
            GunMethod(0);

            flyTimer = 0.9f;

            animator.SetBool("FlyingAnim", true);

            gunShots.Play();

            print("fuck");
        }

        if (isGrounded == true)
        {
            reloadTime += 1.1f * Time.deltaTime;

            transform.rotation = new Quaternion(0,0,0,0);
        }

        if (reloadTime > 1 && isGrounded == true)
        {
            shotsLeft = 2;
        }

        if(isFlying == true)
        {
            flyTimer += 1f * Time.deltaTime;
        }

        /* if(flyTimer >= 1.5f)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        } */


        #endregion

        //TODO Fixa hp

        //Detta ska sänka hp med 1
        if (Input.GetKeyDown(KeyCode.G))
        {
            healthLeft -= 1;
        }


        print(shotsLeft);
    }

    #region Hit detection

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.drag = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "mark")
        {
            animator.SetBool("FlyingAnim", false);

            isGrounded = true;

            rb.drag = 5;

            flyTimer = 0;
        }
    }
    #endregion
    private void GunMethod(int shots)
    {   
        Quaternion gunRotation = gunObject.transform.rotation;

        rb.SetRotation(gunRotation);

        rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

        shotsLeft = shots;

        isGrounded = false;

        isFlying = true;
    }
}
