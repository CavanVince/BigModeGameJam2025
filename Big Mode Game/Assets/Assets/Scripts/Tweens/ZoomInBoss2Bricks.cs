using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInBoss2Bricks : MonoBehaviour
{
    private Vector3 origScale;
    void Start()
    {
        origScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(origScale, 0.5f).SetEase(Ease.Linear);
    }

}
