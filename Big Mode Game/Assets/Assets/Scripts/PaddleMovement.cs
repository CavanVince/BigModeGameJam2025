using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 inputDirection;

    public static PaddleMovement Instance;

    [SerializeField]
    float paddleSpeed;

    [SerializeField]
    float rotationSpeed;

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
        inputDirection = Vector2.zero;

        SpawnBall();
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

        // Gather rotational input
        if (Input.GetKey(KeyCode.LeftArrow) && ((transform.rotation.eulerAngles.z < 45) || (transform.rotation.eulerAngles.z > 305))) 
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.RightArrow) && ((transform.rotation.eulerAngles.z < 55) || transform.rotation.eulerAngles.z > 315)) 
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
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
        rb.velocity = inputDirection * paddleSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Spawn the ball on top of the paddle
    /// </summary>
    public void SpawnBall()
    {
        ballRef = Instantiate(ballPrefab, transform.position + (Vector3.up * 0.35f), Quaternion.identity);
        ballRef.SetParent(transform);
    }
}
