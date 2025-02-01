using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : TrinketParent
{
    //- 1 life 2x mult
    public Dagger()
    {
        BasicLevelManager.Instance.StartingBallCount--;
        BasicLevelManager.Instance.PlayerBallCount = BasicLevelManager.Instance.StartingBallCount;
        BasicLevelManager.Instance.MinScoreMult *= 2;
        BasicLevelManager.Instance.ScoreMult = BasicLevelManager.Instance.MinScoreMult;
    }
}
