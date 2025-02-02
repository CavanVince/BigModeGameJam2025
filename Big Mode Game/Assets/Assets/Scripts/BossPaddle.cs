using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPaddle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform nearestBall;

    [SerializeField]
    float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Very inefficient, but there is a day left and I do not care
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        // Find the nearest ball of all the balls
        nearestBall = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject ball in balls)
        {
            float distance = Vector2.Distance(transform.position, ball.transform.position);
            if (nearestDistance > distance)
            {
                nearestBall = ball.transform;
                nearestDistance = distance;
            }
        }
    }

    private void FixedUpdate()
    {
        if (nearestBall != null)
        {
            Vector2 paddleDir = new Vector2(nearestBall.position.x - transform.position.x, 0);
            rb.velocity = paddleDir * Time.deltaTime * moveSpeed;
        }
    }
}
