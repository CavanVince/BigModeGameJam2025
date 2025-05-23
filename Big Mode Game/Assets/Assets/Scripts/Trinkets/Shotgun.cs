using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns 5 balls at paddle that launch in spread
/// </summary>
public class Shotgun : TrinketParent
{
    public Shotgun()
    {
        AddTrinket();
    }

    public override void TriggerPassive(Transform trans)
    {
        BasicLevelManager.Instance.SpawnBall(trans.position).LaunchBall(new Vector2(1, 0.75f));
        BasicLevelManager.Instance.SpawnBall(trans.position).LaunchBall(new Vector2(1, 0.25f));
        BasicLevelManager.Instance.SpawnBall(trans.position).LaunchBall(new Vector2(-1, 0.75f));
        BasicLevelManager.Instance.SpawnBall(trans.position).LaunchBall(new Vector2(-1, 0.25f));
    }

    public override void RemoveTrinket()
    {
        BasicLevelManager.LaunchedBallFromPaddle -= TriggerPassive;
    }

    public override void AddTrinket()
    {
        BasicLevelManager.LaunchedBallFromPaddle += TriggerPassive;
    }
}
