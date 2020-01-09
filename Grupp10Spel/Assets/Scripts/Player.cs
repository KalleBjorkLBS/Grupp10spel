using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject gunObject = null;
    Rigidbody2D rb = null;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 100f;
    public float shotPower = 10f;

    private bool isGrounded = false;
    private float reloadTime;
    private int shotsLeft;
    private bool isFlying = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       
        
        
        #region Enkel walk + jump
        if (Input.GetKey(KeyCode.D) && isGrounded == true)
        {
            rb.AddForce(new Vector2(5, 0));
        }

        if (Input.GetKey(KeyCode.A) && isGrounded == true)
        {
            rb.AddForce(new Vector2(-5, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0, 10 * jumpMultiplier));
            isGrounded = false;
        }
        #endregion

        #region Gravity 
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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

            reloadTime = 0;
            shotsLeft -= 1;
        }

        print(reloadTime);

        if (Input.GetMouseButtonDown(0) && shotsLeft >= 0 && isFlying == true)
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
