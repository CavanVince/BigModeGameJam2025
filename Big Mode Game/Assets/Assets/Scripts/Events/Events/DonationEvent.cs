using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You have no money to donate right now";
        }
        else
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            return "The man thanks you. You feel good! Donate again? -1 dollar";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney == 0)
        {
            //Add a money trinket
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "Here you need this more then I do";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "The man thanks you for your time and walks away looking sad. You feel bad";
        }
    }
}
