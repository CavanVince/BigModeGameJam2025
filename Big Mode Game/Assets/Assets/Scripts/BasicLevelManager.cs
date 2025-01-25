using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelManager : MonoBehaviour
{
    public static BasicLevelManager Instance;

    public Transform brickParent;

    // The number of balls currently in the world
    public int SpawnedBallCount { get; set; }

    // The number of balls the player has left
    public int PlayerBallCount {  get; private set; }

    private void Start()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }

        SpawnedBallCount = 0;
        PlayerBallCount = 4;
    }

    /// <summary>
    /// Determine if the player destroyed all of the bricks
    /// </summary>
    public void CheckPlayerWon() 
    {
        if (brickParent.childCount == 0) 
        {
            Debug.Log("You Win!");
        }
    }

    /// <summary>
    /// Determine if the player is out of balls
    /// </summary>
    public void CheckGameOver() 
    {
        if (SpawnedBallCount == 0 && PlayerBallCount > 0)
        {
            PaddleMovement.Instance.SpawnBall();
            PlayerBallCount--;
        }
        else if (PlayerBallCount <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
