using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelect : MonoBehaviour
{
    private Vector3 origScale;

    private bool shouldHover;

    public OptionButton Reference;

    private void Awake()
    {
        origScale = transform.localScale;
        shouldHover = true;
       
    }

    private void OnMouseEnter()
    {
        if (!shouldHover) return;
        transform.DOScale(origScale * 1.25f, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }

    private void OnMouseExit()
    {
        if (!shouldHover) return;
        transform.DOScale(origScale, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }

    private void OnMouseDown()
    {
        if (!shouldHover) return;

        switch (Reference)
        {
            case OptionButton.Confirm:
                EventManager.Instance.SelectResult(true);
                
                break;
            case OptionButton.Deny:
                EventManager.Instance.SelectResult(false);
                break;
        }
    }

    public enum OptionButton
    { 
        Confirm,

        Deny
    }

}
