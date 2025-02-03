using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterRocket : TrinketParent
{
    public BoosterRocket() 
    {
        AddTrinket();
    }

    public override void RemoveTrinket()
    {
        PaddleMovement.Instance.paddleSpeed -= 200;
    }

    public override void AddTrinket()
    {
        PaddleMovement.Instance.paddleSpeed += 200;
    }
}
