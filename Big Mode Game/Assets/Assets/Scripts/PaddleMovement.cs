using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public static PaddleMovement Instance;

    public Vector2 InputDirection { get; private set; }


    public float paddleSpeed;


    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        rb = GetComponent<Rigidbody2D>();
        InputDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Gather the input direction
        InputDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            InputDirection -= Vector2.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputDirection += Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        // Apply the velocity
        rb.velocity = InputDirection * paddleSpeed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true && BasicLevelManager.Instance.SpawnedBallCount == 1) 
        {
            // Reset the player's score multiplier when the ball hits the paddle
            BasicLevelManager.Instance.ResetScoreMult();
        }
    }
}
