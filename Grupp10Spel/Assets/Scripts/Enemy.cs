using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Collider2D enemyCollider;
    Animator anim;

    private bool enemyDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
    }
    private void OnParticleCollision(GameObject other)
    {
        enemyCollider.enabled = false;
        anim.SetBool("IsDead", true);
    }
}
