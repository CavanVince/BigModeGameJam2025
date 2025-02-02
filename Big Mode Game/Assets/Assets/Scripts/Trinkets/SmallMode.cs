using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMode : TrinketParent
{
    public SmallMode()
    {
        BasicLevelManager.SpawnedBall += TriggerPassive;
        PlayerInfo.Instance.MinScoreMult += 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult += 2;
    }
    public override void TriggerPassive(Transform trans)
    {
        trans.localScale *= 0.5f;
        trans.GetComponent<TrailRenderer>().startWidth *= 0.5f;
    }

    public override void RemoveTrinket()
    {
        BasicLevelManager.SpawnedBall -= TriggerPassive;
        PlayerInfo.Instance.MinScoreMult -= 2;
        BasicLevelManager.Instance.ScoreMult = PlayerInfo.Instance.MinScoreMult;
    }

}
