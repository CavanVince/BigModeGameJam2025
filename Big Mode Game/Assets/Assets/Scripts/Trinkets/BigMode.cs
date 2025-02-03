using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMode : TrinketParent
{
    public BigMode()
    {
        AddTrinket();
    }
    public override void TriggerPassive(Transform trans)
    {
        trans.localScale *= 2f;
        trans.GetComponent<TrailRenderer>().startWidth *= 2f;
    }

    public override void RemoveTrinket()
    {
           BasicLevelManager.SpawnedBall -= TriggerPassive;
    }

    public override void AddTrinket()
    {
        BasicLevelManager.SpawnedBall += TriggerPassive;
    }
}
