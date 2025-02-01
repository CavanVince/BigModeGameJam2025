using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueBrickBuff : TrinketParent
{
    

    public BlueBrickBuff() 
    {
        BrickParent.BrickHit += TriggerPassive;
    }
    public override void TriggerPassive(Transform brickTransform)
    {
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.BLUE)
        {
            
        BallController tempBall = BasicLevelManager.Instance.SpawnBall(brickTransform.position);
        tempBall.LaunchBall(new Vector2(Random.Range(-1, 1f) * tempBall.ballSpeed, Random.Range(-1, 1f) * tempBall.ballSpeed));
        tempBall.ballHealth = 1;
        }

    }
    // Start is called before the first frame update

}
