using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamblersCoin : TrinketParent
{

    //when you enter a shop you have a chance to lose 5 gold or gain 10 gold.
    public GamblersCoin() 
    {
        BasicLevelManager.EnteredShop += TriggerPassive;
    }

    public override void TriggerPassive()
    {
        PlayerInfo.Instance.PlayerMoney += Random.Range(-6, 11);
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
