using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCap : TrinketParent
{
    BallController controller;
    public WizardCap() 
    {
        BasicLevelManager.LaunchedBallFromPaddle += TriggerPassive;
    }
    public override void TriggerPassive(Transform ballTransform)
    {
        Vector2 diagonalVector = new Vector2(45, 45);
        Rigidbody2D rb = ballTransform.GetComponent<Rigidbody2D>();
        rb.velocity = diagonalVector;
    }
    public override void RemoveTrinket()
    {
        BallController.ballBounced -= TriggerPassive;
    }
}
