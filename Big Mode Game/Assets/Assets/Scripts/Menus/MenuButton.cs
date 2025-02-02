using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private Vector3 origScale;


    private void Awake()
    {
        origScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.DOScale(origScale * 2f, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }


    private void OnMouseExit()
    {
        transform.DOScale(origScale, 0.25f).SetEase(Ease.Linear).SetId("Hover");
    }

}
