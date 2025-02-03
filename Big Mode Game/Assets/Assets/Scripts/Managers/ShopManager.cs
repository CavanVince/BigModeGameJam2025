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

    public Vector3 origTrinketScale;

    public static ShopManager Instance;

    public List<ShopTrinketScriptableObject> trinketScriptableObjects;

    public bool InShop { get; set; } = false;

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
    }

    /// <summary>
    /// Helper function to generate trinkets for the shop
    /// </summary>
    public void GenerateShopTrinkets()
    {
        // Get three unique trinkets from the list
        ShopTrinketScriptableObject soTrinketOne = trinketScriptableObjects[Random.Range(0, trinketScriptableObjects.Count)];
        trinketScriptableObjects.Remove(soTrinketOne);
        ShopTrinketScriptableObject soTrinketTwo = trinketScriptableObjects[Random.Range(0, trinketScriptableObjects.Count)];
        trinketScriptableObjects.Remove(soTrinketTwo);
        ShopTrinketScriptableObject soTrinketThree = trinketScriptableObjects[Random.Range(0, trinketScriptableObjects.Count)];
        trinketScriptableObjects.Remove(soTrinketThree);

        // Setup the shop item UI
        trinketOne.GetComponent<ShopItem>().SetItem(soTrinketOne);
        trinketTwo.GetComponent<ShopItem>().SetItem(soTrinketTwo);
        trinketThree.GetComponent<ShopItem>().SetItem(soTrinketThree);

        // Add the trinkets back to the list
        trinketScriptableObjects.Add(soTrinketOne);
        trinketScriptableObjects.Add(soTrinketTwo);
        trinketScriptableObjects.Add(soTrinketThree);
    }

    /// <summary>
    /// Function to squish trinkets
    /// </summary>
    public void AnimateTrinkets()
    {
        trinketOne.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);
        trinketTwo.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);
        trinketThree.DOScale(origTrinketScale, .1f).SetEase(Ease.Linear).SetDelay(1);

        trinketOne.GetComponent<ShopItem>().shouldHover = true;
        trinketTwo.GetComponent<ShopItem>().shouldHover = true;
        trinketThree.GetComponent<ShopItem>().shouldHover = true;
    }

    /// <summary>
    /// Helper function to zero the scale of the trinkets
    /// </summary>
    public void ZeroTrinkets()
    {
        trinketOne.localScale = new Vector3(trinketOne.localScale.x, 0, trinketOne.localScale.z);
        trinketTwo.localScale = new Vector3(trinketTwo.localScale.x, 0, trinketTwo.localScale.z);
        trinketThree.localScale = new Vector3(trinketThree.localScale.x, 0, trinketThree.localScale.z);
    }
}
