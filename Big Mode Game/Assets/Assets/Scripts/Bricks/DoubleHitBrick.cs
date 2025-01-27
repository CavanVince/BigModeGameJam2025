using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoubleHitBrick : BrickParent
{
    private SpriteRenderer spriteRender;
    private int hitCount = 2;

    [SerializeField]
    Sprite crackedSprite;


    private void Start()
    {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        hitCount--;
        if (hitCount == 1)
        {
            spriteRender.sprite = crackedSprite;
        }
        else
        {
            DestroyBrick();
        }

    }
}
