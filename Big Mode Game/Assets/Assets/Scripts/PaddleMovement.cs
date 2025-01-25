using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 inputDirection;

    [SerializeField]
    float paddleSpeed;


    [SerializeField]
    Rigidbody2D ballRb;

    [SerializeField]
    float ballLaunchSpeed;

    [SerializeField]
    float ballFriction;

    private bool canLaunchBall = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Gather the input direction
        inputDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) 
        {
            inputDirection -= Vector2.right;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            inputDirection += Vector2.right;
        }

        // Launch the ball
        if (Input.GetKeyDown(KeyCode.Space) && canLaunchBall) 
        {
            ballRb.transform.SetParent(null);
            ballRb.simulated = true;
            ballRb.AddForce(Vector2.up * ballLaunchSpeed, ForceMode2D.Impulse);
            canLaunchBall = !canLaunchBall;
        }

    }

    private void FixedUpdate()
    {
        // Apply the velocity
        rb.velocity = inputDirection * paddleSpeed * Time.deltaTime;
    }
}
