using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : TrinketParent
{
    //- 1 life +2 mult
    public Dagger()
    {
        PlayerInfo.Instance.StartingBallCount--;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult += 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
    }

    public override void RemoveTrinket()
    {
        PlayerInfo.Instance.StartingBallCount++;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        PlayerInfo.Instance.MinScoreMult -= 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
    }
}
