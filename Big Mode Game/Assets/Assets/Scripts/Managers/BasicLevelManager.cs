using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelManager : MonoBehaviour
{
    public static BasicLevelManager Instance;

    // The container for the level's brick prefabs
    public Transform brickParent;

    // The number of balls currently in the world
    public int SpawnedBallCount { get; set; }

    // The number of balls the player has left
    public int PlayerBallCount { get; set; }

    #region Player Score
    /// <summary>
    /// The player's score
    /// </summary>
    public int PlayerScore { get; private set; }

    /// <summary>
    /// The score multiplier
    /// </summary>
    public int ScoreMult { get; private set; }
    #endregion

    #region Paddle Ball

    public Transform ballPrefab;

    // Reference to the ball on attached to the paddle
    private Transform paddleBall = null;

    public Action<Transform> LaunchedBallFromPaddle;

    #endregion

    private void Awake()
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



        // Initialize trinkets
        SirBounceAlot sirBounceAlot = new SirBounceAlot();
        Shotgun shotgun = new Shotgun();
        HealthPotion healthPotion = new HealthPotion();
        SpellOfGigantification spell = new SpellOfGigantification();

        // Spawn the starting ball
        SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
    }

    private void Update()
    {
        // Launch the ball if it's attached to the paddle
        if (Input.GetKeyDown(KeyCode.Space) && paddleBall != null)
        {
            LaunchedBallFromPaddle?.Invoke(paddleBall.transform);
            paddleBall.GetComponent<BallController>().LaunchBall(Vector2.up * paddleBall.gameObject.GetComponent<BallController>().ballSpeed);
            paddleBall = null;
        }
    }

    /// <summary>
    /// Spawns a ball at the desired location
    /// </summary>
    /// <param name="spawnLocation">The location to spawn the ball</param>
    /// <param name="spawnOnPaddle">Should the ball be attached to the paddle</param>
    /// <returns></returns>
    public BallController SpawnBall(Vector3 spawnLocation, bool spawnOnPaddle = false)
    {
        SpawnedBallCount++;

        if (spawnOnPaddle)
        {
            paddleBall = Instantiate(ballPrefab, spawnLocation, Quaternion.identity);
            paddleBall.SetParent(PaddleMovement.Instance.transform, true);
            return paddleBall.GetComponent<BallController>();
        }
        else
        {
            return Instantiate(ballPrefab, spawnLocation, Quaternion.identity).GetComponent<BallController>();
        }
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
            SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
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
