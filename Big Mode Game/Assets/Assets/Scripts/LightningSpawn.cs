using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawn: MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(transform.localScale, .667f).SetEase(Ease.Linear).OnComplete(() =>
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
