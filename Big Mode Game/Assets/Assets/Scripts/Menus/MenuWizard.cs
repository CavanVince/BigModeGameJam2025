using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWizard : MonoBehaviour
{

    void Start()
    {
        Vector3 origScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(origScale, .5f).SetDelay(1).SetEase(Ease.Linear).OnComplete(() => { Startup(); });
    }

    private void Startup() 
    {
        transform.DORotate(new Vector3(0, 360, 0), 6, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        MoveLeft();
    }

    private void MoveDown() 
    {
        transform.DOMoveY(-41.24f, 6).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            MoveRight();
        });
    }

    private void MoveRight() 
    {
        transform.GetComponent<RectTransform>().DOMoveX(74.84f, 8).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            MoveUp();
        });
    }

    private void MoveUp() 
    {
        transform.GetComponent<RectTransform>().DOMoveY(41.24f, 6).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            MoveLeft();
        });
    }

    private void MoveLeft() 
    {
        transform.GetComponent<RectTransform>().DOMoveX(-74.84f, 8).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveDown();
        });
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
