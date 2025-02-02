using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pogo : TrinketParent
{
    private int ballLives = 1;
    public Pogo()
    {
        KillBox.HitKillBox += TriggerPassive;
    }
    public override void TriggerPassive(Transform killBoxTransform, Transform ballTransform)
    {
        Debug.Log("passive");
        if (ballLives >= 1)
        {
            killBoxTransform.GetComponent<KillBox>().willKillBall = false;

            ballTransform.GetComponent<Rigidbody2D>().velocity *= (-1 * Vector2.up);
            ballLives--;
        }
        else if(ballLives <= 0) 
        {
            killBoxTransform.GetComponent<KillBox>().willKillBall = true;
            ballLives++;
        }
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }

}
