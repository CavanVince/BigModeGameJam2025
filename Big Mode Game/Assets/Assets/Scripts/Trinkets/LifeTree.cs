using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTree : TrinketParent
{
    /// <summary>
    ///  every 200 brick kills get a perma ball
    /// </summary>
    private int bounceCounter = 0;

    public LifeTree() 
    {
        BallController.ballBounced += TriggerPassive;
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 200)
        {
            BasicLevelManager.Instance.StartingBallCount += 1;
            bounceCounter = 0;  
            //ask cav
            UiManager.Instance.UpdateBallText();
        }
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
