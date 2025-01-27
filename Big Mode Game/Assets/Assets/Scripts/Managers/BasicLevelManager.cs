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
    public int PlayerBallCount { get; private set; }

    /// <summary>
    /// The player's score
    /// </summary>
    public int PlayerScore { get; private set; }

    /// <summary>
    /// The score multiplier
    /// </summary>
    public int ScoreMult { get; private set; }

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

        // Ball count
        SpawnedBallCount = 0;
        PlayerBallCount = 4;

        // Player score
        PlayerScore = 0;
        ScoreMult = 1;
    }

    /// <summary>
    /// Determine if the player destroyed all of the bricks
    /// </summary>
    public void CheckPlayerWon()
    {
        if (brickParent.childCount - 1 == 0)
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
            if (PaddleMovement.Instance != null) PaddleMovement.Instance.SpawnBall();
            PlayerBallCount--;
        }
        else if (PlayerBallCount <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    /// <summary>
    /// Adds score to the player's score multiplied by the player's current score multiplier
    /// </summary>
    /// <param name="score">The new score to add</param>
    public void AddScore(int score)
    {
        PlayerScore += score * ScoreMult;
        ScoreMult++;

        UiManager.Instance.UpdateScoreUI();
    }

    /// <summary>
    /// Resets the player's score multiplier
    /// </summary>
    public void ResetScoreMult()
    {
        ScoreMult = 1;
    }
}
