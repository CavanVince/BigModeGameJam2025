using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BasicLevelManager : MonoBehaviour
{
    public static BasicLevelManager Instance;

    #region Level Info
    [Header("Act Levels")]
    // List of levels for the current act
    [SerializeField]
    private List<GameObject> levels;

    [SerializeField]
    Transform tilemapParent;

    // The container for the level's brick prefabs
    public Transform BrickParent { get; set; }

    // Boolean for completed level status
    private bool beatLevel = false;
    #endregion

    #region Ball Count
    // The number of balls currently in the world
    public int SpawnedBallCount { get; set; } = 0;

    // The number of balls the player has left
    public int PlayerBallCount { get; set; } = 0;

    // The baseline number of balls the player starts with
    public int StartingBallCount { get; set; } = 5;
    #endregion

    public static Action<Transform> SpawnedBall;

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

    /// <summary>
    /// A combo counter
    /// </summary>
    public int ComboCounter { get; set; } = 0;

    /// <summary>
    /// The player's money
    /// </summary>
    public int PlayerMoney { get; set; } = 0;
    #endregion

    #region Paddle Ball

    public Transform ballPrefab;

    // Reference to the ball on attached to the paddle
    private Transform paddleBall = null;

    public static Action<Transform> LaunchedBallFromPaddle;

    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField]
    private AudioClip lifeLost;
    private AudioSource audioSource;
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

        audioSource = GetComponent<AudioSource>();

        // Player score
        PlayerScore = 0;
        ScoreMult = MinScoreMult;
        PlayerBallCount = StartingBallCount;

        // Initialize trinkets
        /*SirBounceAlot sirBounceAlot = new SirBounceAlot();
        Shotgun shotgun = new Shotgun();
        HealthPotion healthPotion = new HealthPotion();
        SpellOfGigantification spell = new SpellOfGigantification();
        Greaseball greaseBall = new Greaseball();
        BoosterRocket boosterRocket = new BoosterRocket();

        */
        GreenBrickBuff greenBrickBuff = new GreenBrickBuff();
        BlueBrickBuff blueBrickBuff = new BlueBrickBuff();
        RedBrickBuff redBrickBuff = new RedBrickBuff();
        // Spawn the starting ball
        //SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
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
        else if (Input.GetKeyDown(KeyCode.Space) && beatLevel)
        {
            PlayerScore = 0;
            ScoreMult = MinScoreMult;
            ComboCounter = 0;
            PlayerBallCount = StartingBallCount;
            UiManager.Instance.UpdateBallText();
            UiManager.Instance.UpdateMoneyText();
            UiManager.Instance.ActivateMapScreen();
            beatLevel = false;
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
            UiManager.Instance.UpdateBallText();
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
        if (BrickParent.childCount - 1 == 0)
        {
            Debug.Log("You Win!");
            DOTween.Kill("Camera Shake");
            DestroyBallsGlobal();
            PlayerMoney += PlayerScore / 2000; // Calculate player money (2000 points = $1)
            UiManager.Instance.ActivatePostLevelScreen(true);
            beatLevel = true;
        }
    }

    /// <summary>
    /// Determine if the player is out of balls
    /// </summary>
    public void CheckGameOver()
    {
        if (SpawnedBallCount == 0 && PlayerBallCount > 0)
        {
            ScreenShake();

            SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
        }
        else if (PlayerBallCount <= 0)
        {
            Debug.Log("Game Over!");
            ScreenShake();
            UiManager.Instance.ActivatePostLevelScreen(false);
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

        ComboCounter = 0;
        UiManager.Instance.UpdateMultUI();
    }

    /// <summary>
    /// Helper function to screen shake
    /// </summary>
    public void ScreenShake()
    {
        
        DOTween.Kill("Camera Shake");
        Camera.main.DOShakePosition(1, 1).SetId("Camera Shake");
        audioSource.clip = lifeLost;
        audioSource.Play();
    }

    /// <summary>
    /// Helper function to load a new level
    /// </summary>
    public void LoadEnemyLevel()
    {
        GameObject nextLevel = levels[UnityEngine.Random.Range(0, levels.Count)];
        levels.Remove(nextLevel);
        GameObject levelInstance = Instantiate(nextLevel);
        levelInstance.transform.SetParent(tilemapParent, true);
        beatLevel = false;
        UiManager.Instance.ActivateLevelScreen(levelInstance.transform);
    }
    public void LoadShop() 
    {
        UiManager.Instance.ActivateShopScreen();
    }
    public void LoadEvent() { }

    /// <summary>
    /// Helper function to destroy all balls, globally :D
    /// </summary>
    private void DestroyBallsGlobal()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        SpawnedBallCount = 0;
    }
}
