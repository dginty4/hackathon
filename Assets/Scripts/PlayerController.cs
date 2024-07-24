using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public int jumps = 2;
    
    // Private variables
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer renderer;
    private float movement;
    private bool facingRight;
    private int jumpCounter;
    
    // Health Variables
    public float healthAmount = 100f;
    public Image HealthBar;
    private bool canTakeDamage = true;
    private float damageCooldown = 1f; 
    
    
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
            DamagePlayer(20f);
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
    
    
    // Health Functions 
    public void DamagePlayer(float damage)
    {
        if (canTakeDamage)
        {
            healthAmount -= damage;
            HealthBar.fillAmount = healthAmount / 100f;

            // Player died
            if (healthAmount <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Start the cooldown timer
            StartCoroutine(StartDamageCooldown());
        }
    }
    
    // Method to Heal Player
    public void HealPlayer(float health)
    {
        healthAmount = 100f;
        HealthBar.fillAmount = healthAmount / 100f;
    }

    IEnumerator StartDamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;    
    }
}
