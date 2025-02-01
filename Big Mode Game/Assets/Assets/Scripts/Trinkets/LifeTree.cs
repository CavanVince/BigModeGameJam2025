using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTree : TrinketParent
{
    private int bounceCounter = 0;

    public LifeTree() 
    {
        BallController.ballBounced += TriggerPassive;
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 100)
        {
            BasicLevelManager.Instance.StartingBallCount += 1;
            bounceCounter = 0;
        }
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
