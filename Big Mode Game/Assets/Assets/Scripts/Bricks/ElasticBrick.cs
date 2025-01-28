using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElasticBrick : BrickParent
{
    [SerializeField]
    float speedMult;

    private Animator animator;
    private float animIntervalTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animIntervalTime = Random.Range(2.5f, 7f);
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer() 
    {
        yield return new WaitForSeconds(animIntervalTime);
        animator.Play($"Base Layer.Bubble Gem Anim", 0, 0.25f);
        StartCoroutine(StartTimer());
    }




    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        collision.gameObject.GetComponent<BallController>().SpeedUp(speedMult);

        GetComponent<BoxCollider2D>().enabled = false;

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
