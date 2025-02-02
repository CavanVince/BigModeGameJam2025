using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishingWellEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney > 0)
        {
            return "You don't have any money to toss away";
        }
        else if (Random.Range(1, 26) < 24)
        {
            PlayerInfo.Instance.PlayerMoney --;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "Better luck next time";
        }
        else
        {
            
            return "Your wish has been answered, a new trinket appears before you";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney > 4)
        {
            return "You walk away money in hand wondering, what if...?";
        }
        else
        {
            return "You walk away knowing you couldn't afford it";
        }
    }
}

