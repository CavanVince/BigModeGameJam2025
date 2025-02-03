using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Every 10 bounces spawn another ball
/// </summary>
public class SirBounceAlot : TrinketParent
{
    private int bounceCounter = 0;

    public SirBounceAlot()
    {
        AddTrinket();
    }

    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 10)
        {
            BallController tempBall = BasicLevelManager.Instance.SpawnBall(ballTransform.position);
            tempBall.LaunchBall(new Vector2(Random.Range(-1, 1f) * tempBall.ballSpeed, Random.Range(-1, 1f) * tempBall.ballSpeed));
            bounceCounter = 0;
        }
    }

    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }

    public override void AddTrinket()
    {
        BallController.ballBounced += TriggerPassive;
    }
}
