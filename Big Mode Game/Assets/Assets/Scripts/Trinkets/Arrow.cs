using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : TrinketParent
{
    
    public Arrow()

    {
        BallController.ballBounced += TriggerPassive;
    }
    public override void TriggerPassive(Transform ballTransform)
    {
            Rigidbody2D rb = ballTransform.GetComponent<Rigidbody2D>();
            BallController ball = rb.GetComponent<BallController>();
        if ( ball.pierceAmount > 0)
        {
            rb.velocity = ball.prevVelocity;
        }
    }
}


