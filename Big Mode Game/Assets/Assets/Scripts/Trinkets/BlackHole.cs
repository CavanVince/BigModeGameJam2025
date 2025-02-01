using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player presses the space bar, detonate a bomb at all of the ball locations
/// </summary>
public class BlackHoleTrinket : TrinketParent
{
    private int bounceCounter = 0;
    public BlackHoleTrinket() 
    {
            BallController.ballBounced += TriggerPassive;
    }

    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 10)
        {
            GameObject explosion = new GameObject();
            explosion.layer = 6;
            explosion.AddComponent<CircleCollider2D>();
            explosion.AddComponent<Rigidbody2D>().gravityScale = 0;
            explosion.transform.position = ballTransform.position;
            explosion.AddComponent<BlackHole>();
        }
    }
    
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
