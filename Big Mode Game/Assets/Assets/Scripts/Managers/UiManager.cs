using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    #region In Game Score
    [Header("In Game Score")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI multText;

    [SerializeField]
    private Animator wizardAnim;

    [SerializeField]
    private TextMeshProUGUI ballCounterText;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    private Transform scoreBox;
    private Vector3 scoreBoxInitScale;
    #endregion

    #region Post Game Analysis
    [Header("Post Game Screen")]
    [SerializeField]
    Transform scoreBackdrop;

    [SerializeField]
    GameObject wizardText;

    [SerializeField]
    TextMeshProUGUI postGameScoreText;

    [SerializeField]
    TextMeshProUGUI postGameComboText;

    [SerializeField]
    TextMeshProUGUI postGameMoneyText;

    [SerializeField]
    List<AudioSource> scoreAudioSources;

    #endregion

    #region Other Screens
    [Header("Alternate Grids")]
    [SerializeField]
    private Transform mapBackdrop;

    [SerializeField]
    private Transform eventBackdrop;

    [SerializeField]
    private Transform shopBackdrop;
    #endregion

    private Transform currentActiveGrid;

    private AudioSource transitionSource;

    void Start()
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

        currentActiveGrid = mapBackdrop;

        scoreBox = scoreText.transform.parent;
        scoreBoxInitScale = scoreBox.localScale;
        transitionSource = GetComponent<AudioSource>();
        UpdateScoreUI(false);
        UpdateBallText();
        UpdateMoneyText();
    }

    /// <summary>
    /// Update the score UI
    /// </summary>
    public void UpdateScoreUI(bool shouldTween = true)
    {
        scoreText.text = "Score: " + BasicLevelManager.Instance.PlayerScore.ToString();
        UpdateMultUI();

        if (!shouldTween) return;

        // Tween the score container
        float scaleMult = BasicLevelManager.Instance.ScoreMult;
        if (scaleMult <= 3)
        {
            scaleMult = 1.15f;
            wizardAnim.Play($"Base Layer.Wizard Grow", 0, 0.25f);
        }
        else if (scaleMult <= 7)
        {
            scaleMult = 1.3f;
            wizardAnim.Play($"Base Layer.Wizard Grow", 0, 0.25f);
        }
        else if (scaleMult <= 15)
        {
            scaleMult = 1.5f;
            wizardAnim.Play($"Base Layer.Wizard Grow", 0, 0.25f);
        }
        else
        {
            scaleMult = 1.75f;
            wizardAnim.Play($"Base Layer.Wizard Shocked", 0, 0.25f);
        }


        DOTween.Kill("Score");
        scoreBox.DOScale(scoreBoxInitScale * scaleMult, 0.15f).SetId("Score").OnComplete(() =>
        {
            scoreBox.DOScale(scoreBoxInitScale, 0.75f).SetId("Score");
        });
    }

    /// <summary>
    /// Update the mult UI
    /// </summary>
    public void UpdateMultUI()
    {
        multText.text = "Mult: " + BasicLevelManager.Instance.ScoreMult.ToString();
    }

    /// <summary>
    /// Update the ball counter text
    /// </summary>
    public void UpdateBallText()
    {
        ballCounterText.text = PlayerInfo.Instance.PlayerBallCount.ToString();
    }

    /// <summary>
    /// Update the money text
    /// </summary>
    public void UpdateMoneyText()
    {
        moneyText.text = "$" + PlayerInfo.Instance.PlayerMoney.ToString();
    }

    /// <summary>
    /// Activate the post level scene
    /// </summary>
    /// <param name="playerWon">Did the player win?</param>
    public void ActivatePostLevelScreen(bool playerWon)
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = scoreBackdrop.transform.position;
        scoreBackdrop.transform.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        scoreBackdrop.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(scoreBackdrop, 0.25f);
        transitionSource.Play();

        postGameScoreText.text = "Score: 0";
        postGameComboText.text = "Max Combo: 0";
        postGameMoneyText.text = "Money: $0";

        scoreBackdrop.DOMoveY(oriPos.y, 0.75f).SetEase(Ease.Linear).SetDelay(0.25f).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake(false);
            StartCoroutine(UpdateUI());
        });
    }

    // Coroutine to update the post level score
    IEnumerator UpdateUI()
    {
        int score = 0;
        float pitch = 1;
        while (score < BasicLevelManager.Instance.PlayerScore)
        {
            int scoreScalar = 1000;
            if (BasicLevelManager.Instance.PlayerScore > 50000)
            {
                scoreScalar = 10000;
            }
            score = Mathf.Clamp(score + scoreScalar, 0, BasicLevelManager.Instance.PlayerScore);
            postGameScoreText.text = "Score: " + score;
            foreach (AudioSource source in scoreAudioSources)
            {
                source.pitch = pitch;
                if (!source.isPlaying)
                {
                    source.Play();
                    pitch = Mathf.Clamp(pitch + 0.025f, 0, 3);
                    break;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);
        postGameComboText.text = "Max Combo: " + BasicLevelManager.Instance.ComboCounter + 1;
        yield return new WaitForSeconds(1);

        int money = 0;
        int totalMoney = BasicLevelManager.Instance.PlayerScore / 2000;
        int moneyScalar = 1;
        if (BasicLevelManager.Instance.PlayerScore > 50000)
        {
            moneyScalar = 10;
        }
        while (money < totalMoney)
        {
            money = Mathf.Clamp(money + moneyScalar, 0, BasicLevelManager.Instance.PlayerScore / 2000);
            postGameMoneyText.text = "Money: $" + money;
            postGameScoreText.text = "Score: " + (BasicLevelManager.Instance.PlayerScore - (money * 2000));
            foreach (AudioSource source in scoreAudioSources)
            {
                source.pitch = pitch;
                if (!source.isPlaying)
                {
                    source.Play();
                    pitch = Mathf.Clamp(pitch - 0.025f, 1, 3);
                    break;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }


    /// <summary>
    /// Helper function to load the map
    /// </summary>
    public void ActivateMapScreen()
    {
        if (!DialogueManager.Instance.InTopRight) DialogueManager.Instance.MoveToTopRight();

        // Move the brick overlay to the top of the screen
        Vector3 oriPos = mapBackdrop.transform.position;
        mapBackdrop.transform.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        mapBackdrop.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(mapBackdrop);
        transitionSource.Play();
        mapBackdrop.DOMoveY(oriPos.y, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake(false);
        });
    }

    /// <summary>
    /// Helper function to load the next level
    /// </summary>
    public void ActivateLevelScreen(Transform level)
    {
        BasicLevelManager.Instance.CanGoToNextScreen = false;

        // Move the brick overlay to the top of the screen
        Vector3 oriPos = level.position;
        level.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        level.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(level);
        transitionSource.Play();
        level.DOMoveY(oriPos.y, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake(false);
            UpdateScoreUI();
            BasicLevelManager.Instance.BrickParent = level.Find("Bricks");
            BasicLevelManager.Instance.SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
        });
    }

    /// <summary>
    /// Helper function to load the shop screen
    /// </summary>
    public void ActivateShopScreen()
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = shopBackdrop.position;
        shopBackdrop.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        shopBackdrop.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        ShopManager.Instance.ZeroTrinkets();
        AnimateCurrentGridDown(shopBackdrop);
        transitionSource.Play();
        shopBackdrop.DOMoveY(oriPos.y, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake(false);
            ShopManager.Instance.GenerateShopTrinkets();
            ShopManager.Instance.AnimateTrinkets();
            ShopManager.Instance.InShop = true;
            BasicLevelManager.Instance.CanGoToNextScreen = true;

            // Wizard dialogue
            DialogueManager.Instance.MoveToBottomCenter("Don't tell my wife I'm selling these...");
        });
    }

    /// <summary>
    /// Helper function to load the shop screen
    /// </summary>
    public void ActivateEvent()
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = eventBackdrop.position;
        eventBackdrop.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        eventBackdrop.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(eventBackdrop);
        transitionSource.Play();
        eventBackdrop.DOMoveY(oriPos.y, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake(false);
            BasicLevelManager.Instance.CanGoToNextScreen = false;

            // Wizard dialogue
            DialogueManager.Instance.EnableWizardSpeak();
            DialogueManager.Instance.StartDialogue("Decisions Decisions...");
        });
    }

    /// <summary>
    /// Helper function to move down the current active grid
    /// </summary>
    /// <param name="newActiveGrid"></param>
    private void AnimateCurrentGridDown(Transform newActiveGrid, float delay = 0)
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = currentActiveGrid.position;

        // Drop down the brick overlay
        currentActiveGrid.DOMoveY(oriPos.y - 30, 0.75f).SetEase(Ease.Linear).SetDelay(delay).OnComplete(() =>
        {
            currentActiveGrid.gameObject.SetActive(false);
            currentActiveGrid.position = oriPos;
            currentActiveGrid = newActiveGrid;
        });
    }

    /// <summary>
    /// Helper function to animate the wizard exiting
    /// </summary>
    public void WizardExit()
    {
        wizardAnim.Play($"Base Layer.Wizard Run", 0, 0f);
    }


    /// <summary>
    /// Helper function to emulate the wizard taking damage
    /// </summary>
    public void TakeDamage()
    {
        wizardAnim.transform.DORotate(new Vector3(0, 360, 0), 0.35f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
        {
            wizardAnim.Play($"Base Layer.Wizard Dizzy", 0, 0f);
        });
    }
}
