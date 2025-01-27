using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private Transform scoreBox;
    private Vector3 scoreBoxInitScale;

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
    }

    /// <summary>
    /// Update the score UI
    /// </summary>
    public void UpdateScoreUI() 
    {
        scoreText.text = BasicLevelManager.Instance.PlayerScore.ToString();

        // Tween the score container
        float scaleMult = BasicLevelManager.Instance.ScoreMult;
        if (scaleMult <= 3)
        {
            scaleMult = 1.25f;
        }
        else if (scaleMult <= 7)
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
}
