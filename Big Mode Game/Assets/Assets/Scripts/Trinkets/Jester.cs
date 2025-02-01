using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : TrinketParent
{
    
    public Jester() 
    {
        BrickParent.BrickHit += TriggerPassive;
    }

    public override void TriggerPassive(Transform brickTransform) 
    {
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.RED)
        {
            BasicLevelManager.Instance.ScoreMult += 4;
        }
    }

    public override void RemoveTrinket()
    {
        BrickParent.BrickHit -= TriggerPassive;
    }
}
