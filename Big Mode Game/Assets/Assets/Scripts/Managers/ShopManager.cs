using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform trinketOne;

    [SerializeField]
    private Transform trinketTwo;

    [SerializeField]
    private Transform trinketThree;

    private Vector3 origTrinketScale;

    public static ShopManager Instance;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        origTrinketScale = trinketOne.localScale;

        trinketOne.localScale = new Vector3(trinketOne.localScale.x, 0, trinketOne.localScale.z);
        trinketTwo.localScale = new Vector3(trinketTwo.localScale.x, 0, trinketTwo.localScale.z);
        trinketThree.localScale = new Vector3(trinketThree.localScale.x, 0, trinketThree.localScale.z);
    }

    /// <summary>
    /// Function to squish trinkets
    /// </summary>
    public void AnimateTrinkets() 
    {
        trinketOne.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);
        trinketTwo.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);
        trinketThree.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);
    }

}
