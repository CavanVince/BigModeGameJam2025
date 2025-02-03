using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticShot : TrinketParent
{
    public ElasticShot()
    {
        BrickParent.BrickHit += TriggerPassive;
    }
    public override void TriggerPassive(Transform brickTransform)
    {
        if (brickTransform.GetComponent<BrickParent>().brickColor == BrickColor.ELASTIC)
        {

            BallController tempBall = BasicLevelManager.Instance.SpawnBall(brickTransform.position);
            tempBall.LaunchBall(new Vector2(Random.Range(-1, 1f) * tempBall.ballSpeed, Random.Range(-1, 1f) * tempBall.ballSpeed));
            tempBall.ballSpeed *= 3;
        }

    }
    public override void RemoveTrinket()
    {
        BrickParent.BrickHit -= TriggerPassive;
    }
}
