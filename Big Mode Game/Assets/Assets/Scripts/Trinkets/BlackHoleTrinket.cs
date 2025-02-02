using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// When the player presses the space bar, detonate a bomb at all of the ball locations
/// </summary>
public class BlackHoleTrinket : TrinketParent
{
    private int bounceCounter = 0;
    private AnimatorController blackHoleAnim;
    public BlackHoleTrinket(AnimatorController anim) 
    {
            BallController.ballBounced += TriggerPassive;
        blackHoleAnim = anim;
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
            explosion.AddComponent<Animator>().runtimeAnimatorController = blackHoleAnim;
            explosion.GetComponent<SpriteRenderer>().sortingOrder = 5;
            explosion.transform.position = ballTransform.position;
            explosion.AddComponent<BlackHole>();
            bounceCounter = 0;
        }
    }
    
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
