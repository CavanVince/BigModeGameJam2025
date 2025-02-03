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
            return "You have no money to donate right now. Press Space to Continue";
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
            UiManager.Instance.UpdateBallText();
            return "A white glow descends towards you. You gain another ball! Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You feel mystical!? Press Space to Continue";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (PlayerInfo.Instance.PlayerMoney < 1)
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You had no money to give anyway. Press Space to Continue";
        }
        else
        {
            EventManager.Instance.YesOption.gameObject.SetActive(false);
            EventManager.Instance.NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "You walk away. Press Space to Continue";
        }
    }
}
