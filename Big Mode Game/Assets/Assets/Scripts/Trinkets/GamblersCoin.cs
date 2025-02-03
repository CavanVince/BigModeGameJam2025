using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamblersCoin : TrinketParent
{

    ///<summary>
    /// when you enter a shop you have a chance to lose 5 gold or gain 10 gold.
    /// </summary>
    public GamblersCoin()
    {
        AddTrinket();
    }

    public override void TriggerPassive()
    {
        PlayerInfo.Instance.PlayerMoney += Random.Range(-6, 11);
        UiManager.Instance.UpdateMoneyText();
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }

    public override void AddTrinket()
    {
        BasicLevelManager.EnteredShop += TriggerPassive;
    }
}
