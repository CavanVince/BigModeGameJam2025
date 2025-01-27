using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticBrick : MonoBehaviour
{
    [SerializeField]
    float speedMult;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true)
        {
            collision.gameObject.GetComponent<BallController>().SpeedUp(speedMult);
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
