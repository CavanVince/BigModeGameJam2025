using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float ballSpeed;
    private Vector2 prevVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prevVelocity = Vector2.zero;

        // Inform the level manager that there is another ball
        BasicLevelManager.Instance.SpawnedBallCount++;
    }

    private void LateUpdate()
    {
        // Store the previous velocity
        if (!(Mathf.Abs(rb.velocity.x) <= 0.15f))
        {
            prevVelocity.x = rb.velocity.x;
        }
        if (!(Mathf.Abs(rb.velocity.y) <= 0.15f))
        {
            prevVelocity.y = rb.velocity.y;
        }

        rb.velocity = rb.velocity.normalized * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ball collided with a brick, exit early
        if (collision.transform.CompareTag("Brick") == true) return;

        // If the ball collided with the paddle, add to it's horizontal velocity and return (simulate friction only for paddle)
        else if (collision.transform.CompareTag("Paddle") == true) 
        {
            rb.velocity += PaddleMovement.Instance.InputDirection * 2;
            return;
        }


        // Jank to prevent directional velocity from being 0 on an axis
        if (Mathf.Abs(rb.velocity.x) <= 0.001f)
        {
            // Determine if velocity is positive or negative
            if (prevVelocity.x > 0)
            {
                rb.velocity += new Vector2(1, 0);
            }
            else
            {
                rb.velocity -= new Vector2(1, 0);
            }
        }

        if (Mathf.Abs(rb.velocity.y) <= 0.001f)
        {
            // Determine if velocity is positive or negative
            if (prevVelocity.y > 0)
            {
                rb.velocity += new Vector2(0, 1);
            }
            else
            {
                rb.velocity -= new Vector2(0, 1);
            }
        }
    }

    /// <summary>
    /// Destroy the ball
    /// </summary>
    public void DestroyBall()
    {
        // Inform the level manager that a ball was destroyed
        BasicLevelManager.Instance.SpawnedBallCount--;

        // Notify the level manager that the ball was destroyed
        BasicLevelManager.Instance.CheckGameOver();

        Destroy(gameObject);
    }

}
