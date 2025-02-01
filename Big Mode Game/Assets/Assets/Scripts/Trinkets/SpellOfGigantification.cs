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
        PaddleMovement.Instance.transform.localScale += new Vector3(10,0,0);
    }

    public override void RemoveTrinket()
    {
        PaddleMovement.Instance.transform.localScale -= new Vector3(10,0,0);
    }
}
