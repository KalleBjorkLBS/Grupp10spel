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

        if (hit.collider != null) //If ray does not hit null, player is near something
        {
            if (hit.collider.transform.tag == "mark" && Player.reloadTime > 1) //If ray hits "mark" reload gun and slows down player velocity
            {
                Player.isGrounded = true;
                Player.rb.velocity = Player.rb.velocity * (0.3f * Time.deltaTime);
            }
        }


        if (hit.collider == null) //If ray hits null, player is flying
        {
            Player.isFlying = true;
        }

        Debug.DrawRay(transform.position, Vector2.down * 4,Color.magenta,3.5f);
    }
}
