using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 5)
        {
            return "You don't have enough money to enter now";
        }
        else if(Random.Range(0, 11) < 10)
        {
            PlayerInfo.Instance.PlayerMoney -= 5;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "Better luck next time";
        }
        else
        {
            PlayerInfo.Instance.PlayerMoney += 50;
            UiManager.Instance.UpdateMoneyText();
            return "Congratulations, you've won our grand prize of 50 dollars!";
        }
    }

    public override string ReturnRejectedResult()
    {
        if(PlayerInfo.Instance.PlayerMoney > 4)
        {
            return "You walk away money in hand wondering, what if...?";
        }
        else
        {
            return "You walk away knowing you couldn't afford it";
        }
    }
}
