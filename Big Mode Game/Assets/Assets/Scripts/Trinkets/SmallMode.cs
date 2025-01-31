using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMode : TrinketParent
{
    public SmallMode()
    {
        BasicLevelManager.SpawnedBall += TriggerPassive;
        BasicLevelManager.Instance.MinScoreMult += 2;
        BasicLevelManager.Instance.ScoreMult = BasicLevelManager.Instance.MinScoreMult;
    }
    public override void TriggerPassive(Transform trans)
    {
        trans.localScale *= 0.5f;
        trans.GetComponent<TrailRenderer>().startWidth *= 0.5f;
    }

}
