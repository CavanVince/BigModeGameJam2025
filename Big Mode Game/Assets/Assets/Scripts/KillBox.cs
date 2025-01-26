using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the ball
        if (collision.transform.CompareTag("Ball") == true) 
        {
            collision.gameObject.GetComponent<BallController>().DestroyBall();
        }
    }
}
