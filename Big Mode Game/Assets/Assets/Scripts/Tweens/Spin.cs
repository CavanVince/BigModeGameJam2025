using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spin : MonoBehaviour
{
    private Vector2 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        transform.DORotate(new Vector3(0, 360, 0), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        BobObject();
    }

    private void BobObject() 
    {
        transform.DOMove(initPos + Vector2.up * 0.5f, 2).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            transform.DOMove(initPos - Vector2.up * 0.5f, 2).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                BobObject();
            });
        });
    }
}
