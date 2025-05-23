using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make the paddle larger
/// </summary>
public class SpellOfGigantification : TrinketParent
{
    public SpellOfGigantification()
    {
        AddTrinket();
    }

    public override void RemoveTrinket()
    {
        PaddleMovement.Instance.transform.localScale -= new Vector3(10, 0, 0);
    }

    public override void AddTrinket()
    {
        PaddleMovement.Instance.transform.localScale += new Vector3(10, 0, 0);
    }
}
