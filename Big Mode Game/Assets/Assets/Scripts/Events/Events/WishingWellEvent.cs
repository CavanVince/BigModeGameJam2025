using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class WishingWellEvent : EventObjectParent
{
    public override string ReturnConfirmedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1 )
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "You don't have any money to wish away right now. Press Space to Continue";
        }
        else if (Random.Range(1, 26) < 24 && PlayerInfo.Instance.PlayerMoney > 0)
        {
            PlayerInfo.Instance.PlayerMoney--;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "Your wishes go unanswered for now. Wish again? -1 Coin";
        }
        else if (PlayerInfo.Instance.AddTrinket(ShopManager.Instance.trinketScriptableObjects[Random.Range(0, ShopManager.Instance.trinketScriptableObjects.Count)]))
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            return "Your wish has been answered, a new trinket appears before you. Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            PlayerInfo.Instance.MinScoreMult += 3;
            UiManager.Instance.UpdateMultUI();
            return "You're feeling very lucky! +3 Mult. Press Space to Continue";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney > 0)
        {
            return "You walk away with nothing. Press Space to Continue";
        }
        else
        {
            return "You walk away knowing you can't afford a wish. Press Space to Continue";
        }
    }
}

