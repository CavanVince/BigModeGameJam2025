using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimewarpDevice : TrinketParent
{
    public TimewarpDevice()
    {
        BasicLevelManager.SpawnedBall += TriggerPassive;
 
    }
    public override void TriggerPassive(Transform trans)
    {
        trans.GetComponent<BallController>().ballSpeed  *= .75f;
    }
}
