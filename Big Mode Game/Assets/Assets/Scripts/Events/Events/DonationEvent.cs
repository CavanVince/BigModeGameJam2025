using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonationEvent : EventObjectParent
{
    private bool donated = false;

    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1)
        {
            return "You have no money. The man thanks you for your time and walks away";
        }
        else if (PlayerInfo.Instance.PlayerMoney > 0)
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            donated = true;
            return "The man thanks you. You feel good! Donate again? -1 dollar";
        }
        else if ((PlayerInfo.Instance.PlayerMoney < 10 || donated) && PlayerInfo.Instance.AddTrinket(ShopManager.Instance.trinketScriptableObjects[Random.Range(0, ShopManager.Instance.trinketScriptableObjects.Count)]))
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "Thank you for your kindness. Have this. You've been award a new trinket";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.MinScoreMult += 3;
            UiManager.Instance.UpdateMultUI();
            return "The man thanks you for your kindness! +3 Mult";
        }
    }

    public override string ReturnRejectedResult()
    {
        EventManager.Instance.YesOption.gameObject.SetActive(false);
        EventManager.Instance.NoOption.gameObject.SetActive(false);
        return "The man thanks you for your time and walks away";
    }
}
