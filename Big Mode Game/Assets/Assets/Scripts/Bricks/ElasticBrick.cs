using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElasticBrick : BrickParent
{
    [SerializeField]
    float speedMult;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        collision.gameObject.GetComponent<BallController>().SpeedUp(speedMult);

        Vector3 startScale = transform.localScale;
        transform.DOScale(startScale * 1.25f, 0.1f).OnComplete(() => 
        {
            transform.DOScale(startScale, 0.1f).OnComplete(() =>
            {
                DestroyBrick();
            });
        });
    }
}
