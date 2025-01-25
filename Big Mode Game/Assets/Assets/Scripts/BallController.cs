using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float ballSpeed;
    private Vector2 prevVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prevVelocity = Vector2.one;

        // Inform the level manager that there is another ball
        BasicLevelManager.Instance.SpawnedBallCount++;
    }

    private void LateUpdate()
    {
        // Jank to prevent directional velocity from being 0 and keeping the move speed constant
        if (Mathf.Abs(rb.velocity.x) <= 0.25f)
        {
            rb.velocity += new Vector2(prevVelocity.x * 1.5f, 0);
        }
        else 
        {
            prevVelocity.x = rb.velocity.x;        
        }

        if (Mathf.Abs(rb.velocity.y) <= 0.25f)
        {
            float yDir = rb.velocity.y >= 0 ? 1 : -1;
            rb.velocity += new Vector2(0, prevVelocity.y * 1.5f);
        }
        else 
        {
            prevVelocity.y = rb.velocity.y;
        }

        rb.velocity = rb.velocity.normalized * ballSpeed;
    }

    private void OnDestroy()
    {
        // Inform the level manager that a ball was destroyed
        BasicLevelManager.Instance.SpawnedBallCount--;

        // Notify the level manager that the ball was destroyed
        BasicLevelManager.Instance.CheckGameOver();
    }

}
