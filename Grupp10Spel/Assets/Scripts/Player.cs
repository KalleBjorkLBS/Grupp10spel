using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject gunObject = null;
    Rigidbody2D rb = null;
    Animator animator;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 100f;
    public float shotPower = 10f;
    private float reloadTime;
    private float flyTimer = 0;
    
    private bool isFlying = false;
    private bool isGrounded = false;

    private int shotsLeft;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Control (KEEP OUT)
        #region Enkel walk + jump

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(3, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-3, 0));
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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

     

        if (Input.GetMouseButtonDown(0) && shotsLeft > 0)
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            isGrounded = false;
            isFlying = true;
            animator.SetBool("FlyingAnim", isFlying);

            reloadTime = 0;
            shotsLeft -= 1;
        }

        if (Input.GetMouseButtonDown(0) && shotsLeft >= 0 && isFlying == true)
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            flyTimer = 0.9f;

            animator.SetBool("FlyingAnim", true);
            
            shotsLeft -= 1;
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

        //todo Få flygning att bli bra genom att fixa drag

        if(isFlying == true)
        {
            flyTimer += 1f * Time.deltaTime;
        }

        if(flyTimer >= 1.5f)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        #endregion

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "mark")
        {
            //isGrounded = true;
            animator.SetBool("FlyingAnim", false);

            rb.drag = 5;

            flyTimer = 0;
        }

        if(collision.gameObject.tag == "enemy")
        {
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.drag = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "mark")
        {
            isGrounded = true;
        }
    }
}
