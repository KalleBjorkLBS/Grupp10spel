using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRay : MonoBehaviour
{
    
    [SerializeField]
    GameObject target = null;

    void Update()
    {
        transform.position = target.transform.position;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f);

        if (hit.collider != null)
        {
            if (hit.collider.transform.tag == "mark" && Player.reloadTime > 1)
            {
                Player.shotsLeft = 2;
                Player.isGrounded = true;
                Player.rb.velocity = Player.rb.velocity * (0.3f * Time.deltaTime);
            }
        }


        if (hit.collider == null)
        {
            Player.isFlying = true;
        }

        //Debug.DrawRay(transform.position, Vector2.down * 4,Color.magenta,3.5f);
    }
}
