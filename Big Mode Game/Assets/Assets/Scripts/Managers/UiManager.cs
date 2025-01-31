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

    [SerializeField] Animator wizardAnim;

    private Transform scoreBox;
    private Vector3 scoreBoxInitScale;
    #endregion

    #region Post Game Analysis
    [Header("Post Game Screen")]
    [SerializeField]
    Transform scoreBackdrop;

    [SerializeField]
    GameObject wizardText;
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
        UpdateScoreUI();
    }

    /// <summary>
    /// Update the score UI
    /// </summary>
    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + BasicLevelManager.Instance.PlayerScore.ToString();
        UpdateMultUI();

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
        AnimateCurrentGridDown(scoreBackdrop);
        scoreBackdrop.DOMoveY(oriPos.y, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake();
        });
    }


    /// <summary>
    /// Helper function to load the map
    /// </summary>
    public void ActivateMapScreen() 
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = mapBackdrop.transform.position;
        mapBackdrop.transform.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        mapBackdrop.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(mapBackdrop);
        mapBackdrop.DOMoveY(oriPos.y, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake();
        });
    }

    /// <summary>
    /// Helper function to load the next level
    /// </summary>
    public void ActivateLevelScreen(Transform level) 
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = level.position;
        level.position = new Vector3(oriPos.x, 30, oriPos.z);

        // Enable the backdrop element
        level.gameObject.SetActive(true);

        // Drop down the brick overlay & current active grid
        AnimateCurrentGridDown(level);
        level.DOMoveY(oriPos.y, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake();
            BasicLevelManager.Instance.BrickParent = level.Find("Bricks");
            BasicLevelManager.Instance.SpawnBall(PaddleMovement.Instance.transform.position + (Vector3.up * 0.5f), true);
        });
    }

    /// <summary>
    /// Helper function to move down the current active grid
    /// </summary>
    /// <param name="newActiveGrid"></param>
    private void AnimateCurrentGridDown(Transform newActiveGrid) 
    {
        // Move the brick overlay to the top of the screen
        Vector3 oriPos = currentActiveGrid.position;

        // Drop down the brick overlay
        currentActiveGrid.DOMoveY(oriPos.y - 30, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            currentActiveGrid.gameObject.SetActive(false);
            currentActiveGrid.position = oriPos;
            currentActiveGrid = newActiveGrid;
        });
    }
}
