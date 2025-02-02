using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEvent : EventObjectParent
{
    private void Awake()
    {
        
    }

    public override string ReturnConfirmedResult()
    {
        if(Random.Range(0, 2)  == 0)
        {
            PlayerInfo.Instance.PlayerMoney += 5;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "The goblin thanks you for your help and gives you 5 dollars!";
        }
        else
        {
            PlayerInfo.Instance.PlayerMoney -= 5;
            UiManager.Instance.UpdateMoneyText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "The goblin is happy for your help and swindles you for 5 dollars";
        }
    }

    public override string ReturnRejectedResult()
    {
        if (Random.Range(0, 2) == 0)
        {
            return "You walk away. The goblin looks really sad";
        }
        else
        {
            PlayerInfo.Instance.StartingBallCount--;
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
            UiManager.Instance.UpdateBallText();
            BasicLevelManager.Instance.CanGoToNextScreen = true;
            return "As you turn to leave the goblin swipes a ball from your back pocket and runs off";
        }
    }
}
