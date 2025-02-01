using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGreenBrickBuff : TrinketParent
{
    public SecondGreenBrickBuff(Collision2D collision)
    {
        if (collision.transform.GetComponent<BaseBrick>().brickColor == BrickColor.GREEN)
        {
            BallController.ballBounced += TriggerPassive;
        }
    }

    public override void TriggerPassive(Transform trans) 
    {
    
    }
}
