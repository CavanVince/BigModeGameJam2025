using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BaseBrick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true)
        {
            DestroyBrick();
        }
    }

    /// <summary>
    /// Check if the player won when the brick is destroyed
    /// </summary>
    private void DestroyBrick()
    {
        BasicLevelManager.Instance.CheckPlayerWon();
        Destroy(gameObject);
    }
}
