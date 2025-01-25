using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BaseBrick : MonoBehaviour
{
    private Animator animator;
    private float animIntervalTime;

    private void Start()
    {
        // Animate brick
        animator = GetComponent<Animator>();
        animIntervalTime = Random.Range(5f, 10f);
        StartCoroutine(PlayAnimation());
    }


    /// <summary>
    /// Coroutine to animate the brick at a random interval
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(animIntervalTime);
        animator.Play("Base Layer.Shine", 0, 0.25f);
        StartCoroutine(PlayAnimation());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Check if the player won when the brick is destroyed
    /// </summary>
    private void OnDestroy()
    {
        BasicLevelManager.Instance.CheckPlayerWon();
    }
}
