using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float previousYDir;

    public float ballSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousYDir = 0;
    }

    private void LateUpdate()
    {
        if (rb.velocity.y.Equals(0))
        {
            rb.velocity += new Vector2(0, previousYDir * 2);
            Debug.Log("hit");
        }
        else
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }

        previousYDir = rb.velocity.y;
    }
    
}
