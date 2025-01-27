using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatTween : MonoBehaviour
{

    void Start()
    {
       StartCoroutine(Delay(Random.Range(0, 5f)));
    }

    void PartOne() 
    {
        transform.DORotate(new Vector3(-3, -3, 3), 4).SetEase(Ease.InOutSine).OnComplete(() => PartTwo());
    }

    // Y needs to rotate faster than x & z
    void PartTwo() 
    {
        transform.DORotate(new Vector3(3, 3, -3), 4).SetEase(Ease.InOutSine).OnComplete(() => PartOne());
    }

    IEnumerator Delay(float delayTime) 
    {
        yield return new WaitForSeconds(delayTime);
        PartOne();
    }
}
