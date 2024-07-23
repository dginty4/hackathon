using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    
    // Private variables
    private Rigidbody2D rb;
    private int jumpCounter = 2;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            // Get input from the player
            var movement = Input.GetAxisRaw("Horizontal");
            var targetPos = transform.position;
            
            // Move
            if (movement != 0.0)
            {
                targetPos.x += movement;
                StartCoroutine(Move(targetPos));
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed), ForceMode2D.Impulse);
                jumpCounter--;
            }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, (moveSpeed * Time.deltaTime));
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {
            jumpCounter = 2;
        }
    }
}
