using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb = null;
    [SerializeField]
    GameObject gunObject = null;
    SpriteRenderer SpriteRenderer = null;

    [SerializeField]
    Sprite playerHP3 = null;
    [SerializeField]
    Sprite playerHP2 = null;
    [SerializeField]
    Sprite playerHP1 = null;


    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 100f;
    public float shotPower = 10f;

    private int playerHealth = 3;

    private bool isGrounded = false;
    private bool isShotRight = false;
    private bool isShotLeft = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SpriteRenderer = GetComponent<SpriteRenderer>();
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
        } else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        

        if (Input.GetMouseButtonDown(0) && (isGrounded == true || isShotLeft == false))
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            isGrounded = false;
            isShotLeft = true;
        }

        if (Input.GetMouseButtonDown(1) && (isGrounded == true || isShotRight == false))
        {
            Quaternion gunRotation = gunObject.transform.rotation;

            rb.SetRotation(gunRotation);

            rb.AddRelativeForce(new Vector2(-100 * shotPower, 0));

            isGrounded = false;
            isShotRight = true;
        }

        if (isGrounded == true)
        {
            rb.SetRotation(0);
        }



        if (Input.GetKey(KeyCode.D) && isGrounded == true)
        {
            rb.AddForce(new Vector2(5, 0));
        }

        if (Input.GetKey(KeyCode.A) && isGrounded == true)
        {
            rb.AddForce(new Vector2(-5, 0));
        }




        if(playerHealth == 3)
        {
            SpriteRenderer.sprite = playerHP3;
        }
        if(playerHealth == 2)
        {
            SpriteRenderer.sprite = playerHP2;
        }
        if(playerHealth == 1)
        {
            SpriteRenderer.sprite = playerHP1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "mark")
        {
            isGrounded = true;

            isShotLeft = false;
            isShotRight = false;
        }

        if(collision.gameObject.tag == "enemy")
        {
            
            
            playerHealth -= 1;
        }
    }


}
