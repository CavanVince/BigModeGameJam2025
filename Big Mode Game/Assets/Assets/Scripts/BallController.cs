using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    /*private Rigidbody2D rb;
    private PhysicsMaterial2D physMat;

    [SerializeField]
    float panelFriction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physMat = rb.sharedMaterial;

        // Initialize the shared material values
        rb.sharedMaterial.friction = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ball is in contact with the paddle and higher than it, set it's friction to a non-zero number
        if (collision.transform.CompareTag("Paddle") && transform.position.y >= collision.transform.position.y)
        {
            physMat.friction = panelFriction;
            rb.sharedMaterial = physMat;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the ball is exiting the paddle, set it's friction to zero
        if (collision.transform.CompareTag("Paddle"))
        {
            physMat.friction = 0;
            rb.sharedMaterial = physMat;

        }
    }*/
}
