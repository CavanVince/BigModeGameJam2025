using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2TopCollider : MonoBehaviour
{
    private Boss2Manager manager;
    private void Start()
    {
        manager = BasicLevelManager.Instance.BossManager.GetComponent<Boss2Manager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true) 
        {
            manager.ReduceHealth();
            PlayerInfo.Instance.PlayerBallCount++;
            collision.gameObject.GetComponent<BallController>().DestroyBall();
        }
    }
}
