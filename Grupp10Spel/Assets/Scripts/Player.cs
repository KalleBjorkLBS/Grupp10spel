﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject gunObject = null;
    public static Rigidbody2D rb = null;
    Animator animator;
    [SerializeField]
    ParticleSystem gunShots = null;
    [SerializeField]
    Camera cam = null;
    [SerializeField]
    GameObject gunShell = null;

    SpriteRenderer playerRendrer = null;

    #region Floats

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 100f;
    public float shotPower = 10f;
    public static float reloadTime;

    #endregion

    #region Bools
    public static bool isFlying = false;
    public static bool isGrounded = false;
    private bool hasShoot = false;

    public static bool hasFarted = false;

    #endregion

    #region Ints
    public static int healthLeft = 3;
    public static int shotsLeft = 2;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRendrer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
     
       
        cam.transform.position = transform.position + (new Vector3(0, 14, -12));
        #region Gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        }
        #endregion
        

        #region Scene

        int sceneId;

        sceneId = SceneManager.GetActiveScene().buildIndex;

        if (sceneId == 1)
        {
            fallMultiplier = 4.5f;
            lowJumpMultiplier = 4.5f;

            shotPower = 25;
        }

        if (sceneId == 2)
        {
            fallMultiplier = 1;
            lowJumpMultiplier = 1;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1)) //Load-Scene 1
        {
           SceneManager.LoadScene(1);
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha2)) //Load-Scene 2
        {
           SceneManager.LoadScene(2);
        }


        if (Input.GetKeyDown(KeyCode.R)) //Load current scene
        {
           SceneManager.LoadScene(sceneId);
        }
        #endregion

        #region Control

        #region Enkel walk + jump

        if (Input.GetKey(KeyCode.D) && isGrounded == true && isFlying == false) //Walk-Right
        {
            transform.position += new Vector3(0.03f, 0, 0);
            rb.freezeRotation = true;
            playerRendrer.flipX = true;
            
        }
        else //If walk == false, you can rotate
        {
            rb.freezeRotation = false;
            animator.SetFloat("WalkingAnim", 0);
        }

        if (Input.GetKey(KeyCode.A) && isGrounded == true && isFlying == false) //Walk-Left
        {
            transform.position += new Vector3(-0.03f, 0, 0);
            rb.freezeRotation = true;
            playerRendrer.flipX = false;
            animator.SetFloat("WalkingAnim", 1);
        }
        else //If walk == false, you can rotate
        {
            rb.freezeRotation = false;
            animator.SetFloat("WalkingAnim", 0);
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded == true) //Animation for walking
        {
            animator.SetFloat("WalkingAnim", 1);
        }
        else
        {
            animator.SetFloat("WalkingAnim", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) //Jump
        {
            rb.AddForce(new Vector2(0, 10 * jumpMultiplier));
            isGrounded = false;
        }

        #endregion

        #region SHOOOOT GUN

        if (Input.GetMouseButtonDown(0) && shotsLeft == 1 && hasShoot == true) //Second shot from gun
        {
            GunMethod(0);

            animator.SetBool("FlyingAnim", true);

            gunShots.Play();
        }

        if (Input.GetMouseButtonDown(0) && shotsLeft == 2) //First shot from gun
        {
            GunMethod(1);

            animator.SetBool("FlyingAnim", isFlying);
            gunShots.Play();

            reloadTime = 0;

            hasShoot = true;
        }

        if (isGrounded == true) //When player lands, reloadTimer and reset roatation
        {
            reloadTime += 1.1f * Time.deltaTime;

            rb.SetRotation(new Quaternion(0, 0, 0, 0));

            hasFarted = false;
        }

        if(reloadTime >1.5f){
            shotsLeft = 2;
            reloadTime = 0;
        }

        #endregion
        #endregion

        if(Input.GetMouseButton(1) && hasFarted == false) //Reset player velocity and rotation
        {
            
        rb.velocity = new Vector2(0,0);
        
        rb.SetRotation(new Quaternion(0,0,0,0));

        hasFarted = true;

         //TODO Insert Fart Animation    

        }

        if(BossFightButton.playerHit == true){
            //TODO Insert dead anim
            //TODO Game Over/Respawn screen overlay
        }

     
    
    }

    #region Hit detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "mark") //Hitting ground
        {
            animator.SetBool("FlyingAnim", false);

            isGrounded = true;

            isFlying = false;
        }

        if (collision.gameObject.tag == "enemy") //Hitting enemy
        {
            rb.AddRelativeForce(new Vector2(-200 * shotPower, 0));

            shotsLeft = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Beam") //Tractor beam 
        {
            transform.position = Vector2.MoveTowards(transform.position, collider.gameObject.transform.position, 2f);
        }
    }

    #endregion
    private void GunMethod(int shots) //Metod handeling recoil of the gun
    {
        Quaternion gunRotation = gunObject.transform.rotation;

        rb.SetRotation(gunRotation);

        rb.AddRelativeForce(new Vector2(-100 * shotPower, 5));

        shotsLeft = shots;

        isGrounded = false;

        isFlying = true;

        GameObject gunShellobject = Instantiate(gunShell);
        gunShellobject.transform.position = transform.position;

        Destroy(gunShellobject, 1f);
    }
}