using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
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
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "Better luck next time";
        }
        else if (PlayerInfo.Instance.AddTrinket(ShopManager.Instance.trinketScriptableObjects[Random.Range(0, ShopManager.Instance.trinketScriptableObjects.Count)]))
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "Your wish has been answered, a new trinket appears before you";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.MinScoreMult += 3;
            UiManager.Instance.UpdateMultUI();
            return "You're feeling very lucky!";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney > 0)
        {
            return "You walk away with nothing.";
        }
        else
        {
            return "You walk away knowing you can't afford a wish";
        }
    }
}

