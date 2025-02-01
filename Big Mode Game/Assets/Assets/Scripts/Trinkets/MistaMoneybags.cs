using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistaMoneybags : TrinketParent
{
    int greenBrickHitCounter = 0;
    public MistaMoneybags()
    {
                BrickParent.BrickHit += TriggerPassive;
    }
    public override void TriggerPassive(Transform brickTransform)
    {
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.GREEN)
        {
            greenBrickHitCounter += 1;
            if (greenBrickHitCounter >= 3) 
            {
                PlayerInfo.Instance.PlayerMoney += 1;
                greenBrickHitCounter = 0;
                UiManager.Instance.UpdateMoneyText();

            }
            
        }
    }
    public override void RemoveTrinket()
    {
        BrickParent.BrickHit -= TriggerPassive;
    }


}
