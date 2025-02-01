using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatTween : MonoBehaviour
{
    [SerializeField]
    private bool randomDelay = true;

    void Start()
    {
        if (randomDelay) StartCoroutine(Delay(Random.Range(0, 5f)));
        else StartCoroutine(Delay(0));

    }

    void PartOne()
    {
        transform.DORotate(new Vector3(-2, -2, 2), 4).SetEase(Ease.InOutSine).OnComplete(() => PartTwo());
    }

    // Y needs to rotate faster than x & z
    void PartTwo()
    {
        transform.DORotate(new Vector3(2, 2, -2), 4).SetEase(Ease.InOutSine).OnComplete(() => PartOne());
    }

    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PartOne();
    }
}
