using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterRocket : TrinketParent
{
    public BoosterRocket() 
    {
        PaddleMovement.Instance.paddleSpeed += 200;
    }
}
