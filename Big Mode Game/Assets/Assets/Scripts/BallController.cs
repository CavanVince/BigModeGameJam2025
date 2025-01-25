using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float ballSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        // Jank to prevent directional velocity from being 0 and keeping the move speed constant
        if (Mathf.Abs(rb.velocity.x) <= 0.25f)
        {
            float xDir = rb.velocity.x >= 0 ? 1 : -1;
            rb.velocity += new Vector2(xDir * 2, 0);
            Debug.Log("Hor");
        }
        if (Mathf.Abs(rb.velocity.y) <= 0.25f)
        {
            float yDir = rb.velocity.y >= 0 ? 1 : -1;
            rb.velocity += new Vector2(0, yDir * 2);
            Debug.Log("Vert");
        }

        rb.velocity = rb.velocity.normalized * ballSpeed;
    }

}
