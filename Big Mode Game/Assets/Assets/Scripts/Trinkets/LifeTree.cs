using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTree : TrinketParent
{
    /// <summary>
    ///  every 100 brick kills get a perma ball
    /// </summary>
    private int bounceCounter = 0;

    public LifeTree()
    {
        AddTrinket();
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 100)
        {
            PlayerInfo.Instance.StartingBallCount += 1;
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
            UiManager.Instance.UpdateBallText();
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
