using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : TrinketParent
{
    
    public Jester() 
    {
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult += 3;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
    }



    public override void RemoveTrinket()
    {
        BrickParent.BrickHit -= TriggerPassive;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult -= 3;
    }
}
