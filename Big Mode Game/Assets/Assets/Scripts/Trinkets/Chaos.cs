using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : TrinketParent
{
    public Chaos()
    {
        AddTrinket();
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        Rigidbody2D rb = ballTransform.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (Random.Range(-1,1f), Random.Range(-1,1f));
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }

    public override void AddTrinket()
    {
        BallController.ballBounced += TriggerPassive;
    }
}
