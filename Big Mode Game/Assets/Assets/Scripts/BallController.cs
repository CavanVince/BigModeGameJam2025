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
        // Jank to prevent directional velocity from being 0 on an axis
        if (Mathf.Abs(rb.velocity.x) <= 0.15f)
        {
            // Determine if velocity is positive or negative
            if (prevVelocity.x > 0)
            {
                rb.velocity += new Vector2(5, 0);
            }
            else 
            {
                rb.velocity -= new Vector2(5, 0);
            }
        }
        else 
        {
            prevVelocity.x = rb.velocity.x;        
        }

        if (Mathf.Abs(rb.velocity.y) <= 0.15f)
        {
            // Determine if velocity is positive or negative
            if (prevVelocity.y > 0)
            {
                rb.velocity += new Vector2(0, 5);
            }
            else
            {
                rb.velocity -= new Vector2(0, 5);
            }
        }
        else 
        {
            prevVelocity.y = rb.velocity.y;
        }

        rb.velocity = rb.velocity.normalized * ballSpeed;
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
