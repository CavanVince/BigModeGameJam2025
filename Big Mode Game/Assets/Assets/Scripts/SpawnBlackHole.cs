using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;

public class BlackHole : MonoBehaviour
{
    private float pullForce;
    private void Start()
    {
        transform.DOScale(transform.localScale * 3.5f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);

        });

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Brick") == true)
        {
            BasicLevelManager.Instance.AddScore(100);
            BasicLevelManager.Instance.ComboCounter++;
            collision.gameObject.GetComponent<BrickParent>().DestroyBrick();

        }

    }


}
