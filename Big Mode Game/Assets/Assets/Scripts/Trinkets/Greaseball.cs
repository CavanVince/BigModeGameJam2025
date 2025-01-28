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
    }
    public override void TriggerPassive(Transform trans) 
    {
        trans.localScale *= 0.75f;
        trans.GetComponent<TrailRenderer>().startWidth *= 0.75f;
        trans.GetComponent<BallController>().ballSpeed += 15;
        BasicLevelManager.Instance.MinScoreMult += 1;
    }

}
