using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoubleHitBrick : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private int hitCount = 2;

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
                spriteRender.color = Color.red;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Check if the player won when the brick is destroyed
    /// </summary>
    private void OnDestroy()
    {
        if (BasicLevelManager.Instance == null) return;

        BasicLevelManager.Instance.CheckPlayerWon();
    }
}
