using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OptionSelect;

public class MenuButton : MonoBehaviour
{
    private Vector3 origScale;


    private void Awake()
    {
        origScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        Debug.Log("hover");
        transform.DOScale(origScale * 1.25f, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }


    private void OnMouseExit()
    {
        transform.DOScale(origScale, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }

}
