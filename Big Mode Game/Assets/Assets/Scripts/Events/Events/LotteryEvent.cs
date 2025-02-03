using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 5)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You don't have enough money to enter now. Press Space to Continue";
        }
        else if(Random.Range(0, 11) < 10)
        {
            PlayerInfo.Instance.PlayerMoney -= 5;
            UiManager.Instance.UpdateMoneyText();
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You win nothing. Better luck next time. Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.PlayerMoney += 50;
            UiManager.Instance.UpdateMoneyText();
            return "Congratulations, you've won our grand prize of 50 dollars! Press Space to Continue";
        }
    }

    public override string ReturnRejectedResult()
    {
        if(PlayerInfo.Instance.PlayerMoney > 4)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You walk away money in hand wondering, what if...? Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You walk away knowing you couldn't afford it. Press Space to Continue";
        }
    }
}
