using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRedBrickBuff : TrinketParent
{
    public SecondRedBrickBuff(Collision2D collision)
    {
        if (collision.transform.GetComponent<BaseBrick>().brickColor == BrickColor.RED)
        {
            if (BasicLevelManager.Instance.MinScoreMult >= 20) 
            {
                BallController.ballBounced += TriggerPassive;
            }   
        }
    }

    public override void TriggerPassive(Transform trans) 
    {
        
    }
}
