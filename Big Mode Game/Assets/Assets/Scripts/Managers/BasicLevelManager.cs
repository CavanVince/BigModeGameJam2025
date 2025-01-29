using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicLevelManager : MonoBehaviour
{
    public static BasicLevelManager Instance;

    // The container for the level's brick prefabs
    public Transform brickParent;

    // The number of balls currently in the world
    public int SpawnedBallCount { get; set; } = 0;

    // The number of balls the player has left
    public int PlayerBallCount { get; set; } = 5;

    public static Action<Transform> SpawnedBall;

    bool levelComplete = false;

    #region Player Score
    /// <summary>
    /// The player's score
    /// </summary>
    public int PlayerScore { get; private set; }

    /// <summary>
    /// The score multiplier
    /// </summary>
    public int ScoreMult { get; set; }

    /// <summary>
    /// The minimum multilpier to apply to the score multiplier
    /// </summary>
    public int MinScoreMult { get; set; } = 1;
    #endregion

    #region Paddle Ball

    public Transform ballPrefab;

    // Reference to the ball on attached to the paddle
    private Transform paddleBall = null;

    public static Action<Transform> LaunchedBallFromPaddle;

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

        // Player score
        PlayerScore = 0;
        ScoreMult = MinScoreMult;

        // Initialize trinkets
        /*SirBounceAlot sirBounceAlot = new SirBounceAlot();
        Shotgun shotgun = new Shotgun();
        HealthPotion healthPotion = new HealthPotion();
        SpellOfGigantification spell = new SpellOfGigantification();
        Greaseball greaseBall = new Greaseball();
        BoosterRocket boosterRocket = new BoosterRocket();*/


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
        else if (levelComplete && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName: "Map");
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
            PlayerBallCount--;
            paddleBall = Instantiate(ballPrefab, spawnLocation, Quaternion.identity);
            SpawnedBall?.Invoke(paddleBall);
            paddleBall.SetParent(PaddleMovement.Instance.transform, true);
            return paddleBall.GetComponent<BallController>();
        }
        else
        {
            Transform newBall = Instantiate(ballPrefab, spawnLocation, Quaternion.identity);
            SpawnedBall?.Invoke(newBall);
            return newBall.GetComponent<BallController>();
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
            UiManager.Instance.ActivatePostLevelScreen(true);
            levelComplete = true;
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
        }
        else if (PlayerBallCount <= 0)
        {
            Debug.Log("Game Over!");
            UiManager.Instance.ActivatePostLevelScreen(false);
            levelComplete = true;
        }
    }

    /// <summary>
    /// Adds score to the player's score multiplied by the player's current score multiplier
    /// </summary>
    /// <param name="score">The new score to add</param>
    public void AddScore(int score)
    {
        PlayerScore += score * ScoreMult;
        ScoreMult += MinScoreMult;

        UiManager.Instance.UpdateScoreUI();
    }

    /// <summary>
    /// Resets the player's score multiplier
    /// </summary>
    public void ResetScoreMult()
    {
        if (SpawnedBallCount > 1) return;
        ScoreMult = MinScoreMult;
        UiManager.Instance.UpdateMultUI();
    }
}
