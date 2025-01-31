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
        multText.text = "Mult: x" + BasicLevelManager.Instance.ScoreMult.ToString();
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

        // Drop down the brick overlay
        scoreBackdrop.DOMoveY(oriPos.y, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BasicLevelManager.Instance.ScreenShake();
        });
    }


    public void ActivateMapScreen() 
    {
            
    }
}
