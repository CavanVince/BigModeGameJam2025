using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public static Action<Transform, Transform> HitKillBox;
    public bool willKillBall = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the ball
        if (collision.transform.CompareTag("Ball") == true)
        {
            Debug.Log(willKillBall);
            HitKillBox?.Invoke(transform, collision.transform);
            if (willKillBall == true)
            {
                collision.gameObject.GetComponent<BallController>().DestroyBall();
            }
        }

    }
}
