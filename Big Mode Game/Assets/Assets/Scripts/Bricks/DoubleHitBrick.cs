using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoubleHitBrick : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private int hitCount = 2;

    [SerializeField]
    Sprite crackedSprite;


    private void Start()
    {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true)
        {
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

    /// <summary>
    /// Check if the player won when the brick is destroyed
    /// </summary>
    public void DestroyBrick() 
    {
        BasicLevelManager.Instance.CheckPlayerWon();
        Destroy(gameObject);
    }
}
