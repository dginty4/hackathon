using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public int jumps = 2;
    public int lives = 3;
    
    // Private variables
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer renderer;
    private float movement;
    private bool facingRight;
    private int jumpCounter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        jumpCounter = jumps;
    }

    void Update()
    {
        // Get input from the player
        movement = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
            Jump();
        
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    
    void FixedUpdate()
    {
        Move(movement);        
    }
    
    void Jump()
    {
        if (jumpCounter > 0)
        {
            jumpCounter--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("jumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Landing from jump
        if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {
            jumpCounter = 2;
            animator.SetBool("jumping", false);
        }
        
        // Handle touching an obstacle
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            if (lives > 0)
                lives--;
            else
                Debug.Log("Dead"); // TODO: handle dying
        }
    }

    void Move(float dir)
    {
        float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;
 
        //If looking right and clicked left (flip to the left)
        if(facingRight && dir < 0)
        {
            renderer.flipX = true;
            facingRight = false;
        }
        //If looking left and clicked right (flip to the right)
        else if(!facingRight && dir > 0)
        {
            renderer.flipX = false;
            facingRight = true;
        }
        
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }   
}
