using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the player's balls faster but smaller
/// </summary>
public class Greaseball : TrinketParent
{
    public Greaseball() 
    {
        BasicLevelManager.SpawnedBall += TriggerPassive;
        BasicLevelManager.Instance.MinScoreMult += 1;
        BasicLevelManager.Instance.ScoreMult = BasicLevelManager.Instance.MinScoreMult;
    }
    public override void TriggerPassive(Transform trans) 
    {
        trans.localScale *= 0.75f;
        trans.GetComponent<TrailRenderer>().startWidth *= 0.75f;
        trans.GetComponent<BallController>().ballSpeed += 15;
    }

}
