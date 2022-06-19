using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{



    [SerializeField]
    float health, maxHealth = 3.0f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        animator.SetBool("Died", health <= 0);
        
    }

    public void DestroyGameObject()
    {
        //todo: emmit particle effects
        //
        Destroy(gameObject);
    }
}
