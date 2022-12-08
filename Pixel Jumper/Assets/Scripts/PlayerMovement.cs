using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed     = 5f;
    [SerializeField] float jumpHeight   = 5f;
    [SerializeField] float climbSpeed   = 5f;
    [SerializeField] Vector2 deathKick  = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform  gun;

    Vector2             moveInput;
    Rigidbody2D         myRigidbody;
    Animator            myAnimator;
    CapsuleCollider2D   myBodyCollider;
    BoxCollider2D       myFeetCollider;
    float               gravityScaleAtStart;
    bool                isAlive = true;

    void Start()
    {
        myRigidbody         = GetComponent<Rigidbody2D>();
        myAnimator          = GetComponent<Animator>();
        myBodyCollider      = GetComponent<CapsuleCollider2D>();
        myFeetCollider      = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!isAlive){ return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    void OnFire(InputValue value)
    {
        if(!isAlive)
        { 
            return;
        }
        
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if(!isAlive){ return;}
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if(!isAlive){ return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpHeight);
        }
    }
    
    void Run()
    {
        Vector2 playerVelocity          = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity            = playerVelocity;
        bool playerHasHorizontalSpeed   = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        { 
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity          = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity           = climbVelocity;
        myRigidbody.gravityScale       = 0f;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
        }
    }

    
}
