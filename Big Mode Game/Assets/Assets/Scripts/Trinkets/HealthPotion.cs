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
        AddTrinket();
    }

    public override void RemoveTrinket()
    {
        PlayerInfo.Instance.StartingBallCount--;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
    }

    public override void AddTrinket()
    {
        PlayerInfo.Instance.StartingBallCount++;
        PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
        UiManager.Instance.UpdateBallText();
    }
}
