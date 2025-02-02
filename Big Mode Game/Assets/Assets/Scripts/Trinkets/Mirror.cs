using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : TrinketParent
{ 
    public Mirror()

    {
        BasicLevelManager.LaunchedBallFromPaddle += TriggerPassive;
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        Console.Write("in Passive");
        if (BasicLevelManager.Instance.paddleBall != null) 
        {
            Console.Write("in if");
            BasicLevelManager.Instance.paddleBall.GetComponent<BallController>().LaunchBall
                (Vector2.up * BasicLevelManager.Instance.paddleBall.gameObject.GetComponent<BallController>().ballSpeed);
        }
    }
    public override void RemoveTrinket()
    {
        
    }
    private void playerLaunchedBall() 
    {
        
    }

}
