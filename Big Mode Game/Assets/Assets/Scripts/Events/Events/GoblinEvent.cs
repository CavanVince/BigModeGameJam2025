using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if(Random.Range(0, 2)  == 0)
        {
            PlayerInfo.Instance.PlayerMoney += 5;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "The goblin thanks you for your help and gives you 5 dollars!. Press Space to Continue";
        }
        else
        {
            if(PlayerInfo.Instance.PlayerMoney >= 5)
            {
                PlayerInfo.Instance.PlayerMoney -= 5;
                UiManager.Instance.UpdateMoneyText();
                EventManager.Instance.YesOption.gameObject.SetActive(false);
                EventManager.Instance.NoOption.gameObject.SetActive(false);
                BasicLevelManager.Instance.CanGoToNextScreen = true;
                return "You've been tricked! The goblin swindles you for 5 dollars. Press Space to Continue";
            }
            else
            {
                PlayerInfo.Instance.PlayerMoney -= PlayerInfo.Instance.PlayerMoney;
                UiManager.Instance.UpdateMoneyText();
                EventManager.Instance.YesOption.gameObject.SetActive(false);
                EventManager.Instance.NoOption.gameObject.SetActive(false);
                BasicLevelManager.Instance.CanGoToNextScreen = true;
                return "You've been tricked! The goblin takes everything you have. Press Space to Continue";
            } 
        }
    }

    public override string ReturnRejectedResult()
    {
        if (Random.Range(0, 2) == 0)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You walk away. The goblin looks really sad. Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.StartingBallCount--;
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
            UiManager.Instance.UpdateBallText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "The goblin is angered by your refusal to help and swipes a ball from your back pocket before running off. Press Space to Continue";
        }
    }
}
