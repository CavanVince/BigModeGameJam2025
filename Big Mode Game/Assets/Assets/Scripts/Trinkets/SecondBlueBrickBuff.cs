using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBlueBrickBuff : TrinketParent
{
    public SecondBlueBrickBuff(Collision2D collision)
    {
        if (collision.transform.GetComponent<BaseBrick>().brickColor == BrickColor.BLUE)
        {
            BallController.ballBounced += TriggerPassive;
        }
    }

    public override void TriggerPassive(Transform trans) 
    {
    
    }

}
