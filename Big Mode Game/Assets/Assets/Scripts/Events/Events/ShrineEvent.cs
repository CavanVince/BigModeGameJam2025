using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You have no money to donate right now";
        }
        else if(Random.Range(1,6) > 4)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            PlayerInfo.Instance.StartingBallCount++;
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
            return "A white glow descends towards you. You gain another ball!";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You feel mystical!?";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1)
        {
            //Add a money trinket
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You had no money to give anyway";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You walk away";
        }
    }
}
