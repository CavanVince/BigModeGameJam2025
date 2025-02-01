using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private Vector3 origScale;
    private ShopTrinketScriptableObject currentTrinketSo;
    private bool shouldHover;

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip buyItem;

    [SerializeField]
    AudioClip cantBuyItem;

    private AudioSource audioSource;

    private void Awake()
    {
        origScale = transform.localScale;
        shouldHover = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if (!shouldHover) return;
        transform.DOScale(origScale * 1.25f, 0.25f).SetEase(Ease.Linear).SetId("Hover");
        DialogueManager.Instance.StartDialogue(currentTrinketSo.trinketDescription);
    }

    private void OnMouseExit()
    {
        if (!shouldHover) return;
        transform.DOScale(origScale, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }

    

    private void OnMouseDown()
    {
        if (!shouldHover) return; // Making double use of boolean to prevent buying same item multiple times while animating

        if (PlayerInfo.Instance.PlayerMoney >= currentTrinketSo.trinketPrice)
        {
            if (PlayerInfo.Instance.AddTrinket(currentTrinketSo))
            {
                DOTween.Kill("Hover");
                shouldHover = false;
                PlayerInfo.Instance.PlayerMoney -= currentTrinketSo.trinketPrice;
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear);
                audioSource.clip = buyItem;
                audioSource.Play();
            }
            else 
            {
                //Player has too many trinkets, inform them how to remove one
                DOTween.Kill("Shake Item");
                transform.DOShakePosition(0.5f).SetId("Shake Item");
                audioSource.clip = cantBuyItem;
                audioSource.Play();
                DialogueManager.Instance.StartDialogue("You have too many trinkets, left click on one to remove it");
            }
        }
        else
        {
            // Player is too broke, roast them >:)
            DOTween.Kill("Shake Item");
            transform.DOShakePosition(0.5f).SetId("Shake Item");
            audioSource.clip = cantBuyItem;
            audioSource.Play();
            DialogueManager.Instance.StartDialogue(new string[]{"Get your bread up", "Sorry bud, that's for Mr. Moneybags", "I knew a homeless guy with more money than you", "That's too expensive"});
        }

    }

    /// <summary>
    /// Helper function to set the shop item's values
    /// </summary>
    /// <param name=""></param>
    public void SetItem(ShopTrinketScriptableObject trinketSo)
    {
        transform.Find("Trinket Sprite").GetComponent<Image>().sprite = trinketSo.trinketSprite;
        transform.Find("Item Cost Text").GetComponent<TextMeshProUGUI>().text = "$" + trinketSo.trinketPrice.ToString();
        currentTrinketSo = trinketSo;
    }
}
