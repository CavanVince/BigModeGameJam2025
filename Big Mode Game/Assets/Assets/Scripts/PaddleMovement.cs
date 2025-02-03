using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public static PaddleMovement Instance;

    public Vector2 InputDirection { get; private set; }


    public float paddleSpeed;

    private bool invincible = false;
    private const float invTimer = 3f;

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
        if (collision.transform.CompareTag("Ball") == true)
        {
            // Reset the player's score multiplier when the ball hits the paddle
            BasicLevelManager.Instance.ResetScoreMult();
        }
        else if (invincible == false && collision.transform.CompareTag("Brick") == true) // Check for brick (used for 3rd boss)
        {
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.PlayerBallCount - 1; 
            

            if (PlayerInfo.Instance.PlayerBallCount < 0)
            {
                BasicLevelManager.Instance.DestroyBallsGlobal();
                BasicLevelManager.Instance.CheckGameOver();
            }
            else
            {
                BasicLevelManager.Instance.ScreenShake();
            }
            PlayerInfo.Instance.PlayerBallCount = Mathf.Clamp(PlayerInfo.Instance.PlayerBallCount, 0, int.MaxValue);
            UiManager.Instance.UpdateBallText();
            invincible = true;
            StartCoroutine(IFrameTimer());
        }
    }

    public IEnumerator IFrameTimer() 
    {
        invincible = true;
        yield return new WaitForSeconds(invTimer);
        invincible = false;
    }

    public void DisablePaddle() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnablePaddle()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
