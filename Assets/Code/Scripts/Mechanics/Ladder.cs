using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 2f;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private float originalGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    private void Update()
    {
        if(isClimbing)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);

            rb.gravityScale = 0f;

            if (verticalInput == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);

                //freeze movement if the player is in middle of the ladder
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Ladder"))
        {
            Debug.Log("Player on Ladder");
            isClimbing = true;
            rb.gravityScale = 0f;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = originalGravity;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Out of Ladder");
        }
    }
}
