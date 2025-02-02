using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney == 0)
        {
            //Add a money trinket

            return "Here you need this more then I do";
        }
        else
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "The man thanks you. You feel good!";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney == 0)
        {
            //Add a money trinket

            return "Here you need this more then I do";
        }
        else
        {
            return "The man thanks you for your time and walks away. You feel bad";
        }
    }
}
