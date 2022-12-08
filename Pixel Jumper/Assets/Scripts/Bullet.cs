using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed = 10f;
    float xSpeed;
    PlayerMovement player;
   
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player      = FindObjectOfType<PlayerMovement>();
        xSpeed      = player.transform.localScale.x * bulletSpeed;
    }

    
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);  // destroys the enemy
        }

        Destroy(gameObject);            // destroys the bullet on impact
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);            // destroy the bullet if it hits a wall/floor
    }
}
