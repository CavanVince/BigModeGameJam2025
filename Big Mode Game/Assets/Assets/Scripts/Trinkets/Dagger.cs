using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : TrinketParent
{
    //- 1 life +2 mult
    public Dagger()
    {
        PlayerInfo.Instance.StartingBallCount -= 3;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult += 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
        BallController.ballBounced += TriggerPassive;
    }

    public override void TriggerPassive(Transform ballTransform)
    {
        Rigidbody2D rb = ballTransform.GetComponent<Rigidbody2D>();
        BallController ball = rb.GetComponent<BallController>();
        if (ball.pierceAmount > 0)
        {
            rb.velocity = ball.prevVelocity;
        }
    }


    public override void RemoveTrinket()
    {
        PlayerInfo.Instance.StartingBallCount++;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult -= 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
        BrickParent.BrickHit -= TriggerPassive;
    }

}
