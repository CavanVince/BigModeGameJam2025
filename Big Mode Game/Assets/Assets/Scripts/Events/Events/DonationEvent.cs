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
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You have no money. The man thanks you for your time and walks away. Press Space to Continue";
        }
        else if ((PlayerInfo.Instance.PlayerMoney < 10 || donated) && PlayerInfo.Instance.AddTrinket(ShopManager.Instance.trinketScriptableObjects[Random.Range(0, ShopManager.Instance.trinketScriptableObjects.Count)]))
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "Thank you for your kindness. Have this. You've been awarded a new trinket Press Space to Continue";
        }
        else if (PlayerInfo.Instance.PlayerMoney > 0)
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            donated = true;
            return "The man thanks you. You feel good! Donate again? -1 dollar";
        }
        else
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.MinScoreMult += 3;
            UiManager.Instance.UpdateMultUI();
            return "The man thanks you for your kindness! +3 Mult. Press Space to Continue";
        }
    }

    public override string ReturnRejectedResult()
    {
        EventManager.Instance.YesOption.gameObject.SetActive(false);
        EventManager.Instance.NoOption.gameObject.SetActive(false);
        return "The man thanks you for your time and walks away. Press Space to Continue";
    }
}
