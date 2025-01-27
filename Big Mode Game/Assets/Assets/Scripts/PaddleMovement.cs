using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public static PaddleMovement Instance;

    public Vector2 InputDirection { get; private set; }

    [SerializeField]
    float paddleSpeed;

    [SerializeField]
    Transform ballPrefab;
    private Transform ballRef;

    // Start is called before the first frame update
    void Start()
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

        SpawnBall();
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


        // Launch the ball
        if (Input.GetKeyDown(KeyCode.Space) && ballRef != null)
        {
            ballRef.SetParent(null);
            Rigidbody2D ballRb = ballRef.GetComponent<Rigidbody2D>();
            ballRb.simulated = true;
            ballRb.AddForce(Vector2.up * ballRb.gameObject.GetComponent<BallController>().ballSpeed, ForceMode2D.Impulse);
            ballRef = null;
        }
    }

    private void FixedUpdate()
    {
        // Apply the velocity
        rb.velocity = InputDirection * paddleSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Spawn the ball on top of the paddle
    /// </summary>
    public void SpawnBall()
    {
        ballRef = Instantiate(ballPrefab, transform.position + (Vector3.up * 0.35f), Quaternion.identity);
        ballRef.SetParent(transform, true);
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
