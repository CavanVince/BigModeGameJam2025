using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    #region In Game Score
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI multText;

    private Transform scoreBox;
    private Vector3 scoreBoxInitScale;
    #endregion

    #region Post Game Analysis
    [SerializeField]
    Image levelReportBackdrop;

    [SerializeField]
    RectTransform brickBackdrop;

    [SerializeField]
    Transform wizardBox;

    [SerializeField]
    GameObject wizardText;
    #endregion

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
        }
        else if (scaleMult <= 7)
        {
            scaleMult = 1.3f;
        }
        else if (scaleMult <= 15)
        {
            scaleMult = 1.5f;
        }
        else
        {
            scaleMult = 1.75f;
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
        multText.text = "Mult: x" + BasicLevelManager.Instance.ScoreMult.ToString();
    }


    /// <summary>
    /// Activate the post level scene
    /// </summary>
    /// <param name="playerWon">Did the player win?</param>
    public void ActivatePostLevelScreen(bool playerWon)
    {
        // Turn down the alpha of the backdrop
        Color oriBackdropColor = levelReportBackdrop.color;
        Color backdropColor = oriBackdropColor;
        backdropColor.a = 0;
        levelReportBackdrop.color = backdropColor;

        // Move the brick overlay to the top of the screen
        float anchoredPos = brickBackdrop.anchoredPosition.y;
        brickBackdrop.anchoredPosition = new Vector3(0, -brickBackdrop.rect.y * 2, 0);

        // Squash the wizard box
        Vector3 wizardScale = wizardBox.localScale;
        wizardBox.localScale = Vector3.zero;

        // Enable the UI element
        levelReportBackdrop.gameObject.SetActive(true);

        // Slowly increase the alpha of the backdrop
        levelReportBackdrop.DOColor(oriBackdropColor, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Drop down the brick overlay
            brickBackdrop.DOMoveY(anchoredPos, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                // Scale up wizard box
                wizardBox.DOScale(wizardScale, 0.25f).SetDelay(0.5f).OnComplete(() =>
                {
                    wizardBox.GetComponent<FloatTween>().enabled = true;
                });
            });
        });
    }
}
