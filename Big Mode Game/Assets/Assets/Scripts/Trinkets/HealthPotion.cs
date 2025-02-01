using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives the player an extra shot per level
/// </summary>
public class HealthPotion : TrinketParent
{
    public HealthPotion()
    {
        BasicLevelManager.Instance.StartingBallCount++;
        BasicLevelManager.Instance.PlayerBallCount = BasicLevelManager.Instance.StartingBallCount;
    }
}
