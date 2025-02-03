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

    // The boss level of the act
    [SerializeField]
    private GameObject bossLevel;
    public BossManagerParent BossManager { get; set; } = null;

    [SerializeField]
    Transform tilemapParent;

    // The container for the level's brick prefabs
    public Transform BrickParent { get; set; }

    // Boolean for completed level status
    public bool CanGoToNextScreen { get; set; } = false;
    #endregion

    #region Ball Count
    // The number of balls currently in the world
    public int SpawnedBallCount { get; set; } = 0;

    #endregion

    public static Action<Transform> SpawnedBall;

    public static Action EnteredShop;

    #region Player Score
    /// <summary>
    /// The player's score
    /// </summary>
    public int PlayerScore { get; set; }

    /// <summary>
    /// The score multiplier
    /// </summary>
    public int ScoreMult { get; set; }

    /// <summary>
    /// A combo counter
    /// </summary>
    public int ComboCounter { get; set; } = 0;
    #endregion

    #region Paddle Ball

    public Transform ballPrefab;

    // Reference to the ball on attached to the paddle
    public Transform paddleBall = null;

    public static Action<Transform> LaunchedBallFromPaddle;

    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField]
    private AudioClip lifeLost;
    private AudioSource audioSource;
    #endregion


    public bool CanInput = true;

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
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;

        audioSource = GetComponent<AudioSource>();

        // Player score
        PlayerScore = 0;
    }

    private void Start()
    {
        ScoreMult = PlayerInfo.Instance.MinScoreMult;

        // Initialize trinkets
        /*SirBounceAlot sirBounceAlot = new SirBounceAlot();
        Shotgun shotgun = new Shotgun();
        HealthPotion healthPotion = new HealthPotion();*/


        /*SpellOfGigantification spell = new SpellOfGigantification();
        Greaseball greaseBall = new Greaseball();
        BoosterRocket boosterRocket = new BoosterRocket();*/


        //MistaMoneybags greenBrickBuff = new MistaMoneybags();
        //GhostBall blueBrickBuff = new GhostBall();
        //Jester redBrickBuff = new Jester();

        //LightningStrikeTrinket lightningStrikeTrinket = new LightningStrikeTrinket(lightningStrikeSprite);
        //BlackHoleTrinket blackHoleTrinket = new BlackHoleTrinket(blackHoleSprite);

        //WizardCap wizardCap = new WizardCap();
        //Chaos chaos = new Chaos();
        //Pogo pogo = new Pogo();
        //Arrow arrow = new Arrow();
        //Mirror mirror = new Mirror();
        //ElasticShot elasticShot = new ElasticShot();
        //SplitBall splitBall = new SplitBall();
    }

    private void Update()
    {
        // Can the user shoot the ball?
        if (!CanInput) return;

        if (Input.GetKeyDown(KeyCode.Space) && paddleBall != null)
        {
            LaunchedBallFromPaddle?.Invoke(paddleBall.transform);
            paddleBall.GetComponent<BallController>().LaunchBall(Vector2.up * paddleBall.gameObject.GetComponent<BallController>().ballSpeed);
            paddleBall = null;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && CanGoToNextScreen)
        {
            PlayerInfo.Instance.PlayerBallCount = PlayerInfo.Instance.StartingBallCount;
            PlayerScore = 0;
            ScoreMult = PlayerInfo.Instance.MinScoreMult;
            ComboCounter = 0;
            UiManager.Instance.UpdateBallText();
            UiManager.Instance.UpdateMoneyText();
            UiManager.Instance.ActivateMapScreen();
            CanGoToNextScreen = false;
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
            PlayerInfo.Instance.PlayerBallCount--;
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
        if (BossManager != null && BrickParent.childCount - 1 == 0)
        {
            PlayerInfo.Instance.PlayerMoney += 50;
            BossManager.EndLevel();
        }
        else if (BrickParent.childCount - 1 == 0)
        {
            Debug.Log("You Win!");
            DOTween.Kill("Camera Shake");
            DestroyBallsGlobal();
            PlayerInfo.Instance.PlayerMoney += PlayerScore / 2000; // Calculate player money (2000 points = $1)
            UiManager.Instance.ActivatePostLevelScreen(true);
            CanGoToNextScreen = true;
        }
    }

    /// <summary>
    /// Determine if the player is out of balls
    /// </summary>
    public void CheckGameOver()
    {
        if (SpawnedBallCount == 0 && PlayerInfo.Instance.PlayerBallCount > 0)
        {
            ScreenShake();

            SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);

            // Iframe stuff for boss 3
            StartCoroutine(PaddleMovement.Instance.IFrameTimer());
        }
        else if (SpawnedBallCount == 0 && PlayerInfo.Instance.PlayerBallCount <= 0)
        {
            Debug.Log("Game Over!");
            ScreenShake();
            UiManager.Instance.ActivatePostLevelScreen(false);

            DOTween.Kill("Boss 3"); // Kills tweening on boss 3
            DestroyBallsGlobal(); // More boss 3 cleanup
        }
    }

    /// <summary>
    /// Adds score to the player's score multiplied by the player's current score multiplier
    /// </summary>
    /// <param name="score">The new score to add</param>
    public void AddScore(int score)
    {
        PlayerScore += score * ScoreMult;
        ScoreMult += PlayerInfo.Instance.MinScoreMult;

        UiManager.Instance.UpdateScoreUI();
    }

    /// <summary>
    /// Resets the player's score multiplier
    /// </summary>
    public void ResetScoreMult()
    {
        if (SpawnedBallCount > 1) return;
        ScoreMult = PlayerInfo.Instance.MinScoreMult;

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
        CanGoToNextScreen = false;
        UiManager.Instance.ActivateLevelScreen(levelInstance.transform);
    }
    public void LoadShop()
    {
        UiManager.Instance.ActivateShopScreen();
        EnteredShop?.Invoke();
    }

    /// <summary>
    /// Helper function to load an event
    /// </summary>
    public void LoadEvent()
    {
        UiManager.Instance.ActivateEvent();
    }

    /// <summary>
    /// Helper function to load the boss level
    /// </summary>
    public void LoadBoss()
    {
        GameObject levelInstance = Instantiate(bossLevel);
        levelInstance.transform.SetParent(tilemapParent, true);
        CanGoToNextScreen = false;
        UiManager.Instance.ActivateLevelScreen(levelInstance.transform);
    }

    /// <summary>
    /// Helper function to destroy all balls, globally :D
    /// </summary>
    public void DestroyBallsGlobal()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        SpawnedBallCount = 0;
    }
}
