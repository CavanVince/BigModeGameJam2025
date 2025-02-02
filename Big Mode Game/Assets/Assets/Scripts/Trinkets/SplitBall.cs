using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : TrinketParent
{
    private bool blue = false;
    private bool green = false;
    private bool red = false;
    public SplitBall()
    {
        BrickParent.BrickHit += TriggerPassive;
    }
    public override void TriggerPassive(Transform brickTransform)
    {
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.BLUE)
        {
            blue = true;

        }
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.RED)
        {
            red = true;

        }
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.GREEN)
        {
            green = true;

        }
        if (blue && green && red)
        {
            BallController tempBall = BasicLevelManager.Instance.SpawnBall(brickTransform.position);
            tempBall.LaunchBall(new Vector2(Random.Range(-1, 1f) * tempBall.ballSpeed, Random.Range(-1, 1f) * tempBall.ballSpeed));
            blue = false;
            green = false;
            red = false;
        }
    }
    public override void RemoveTrinket()
    {
        BrickParent.BrickHit -= TriggerPassive;
    }
}
