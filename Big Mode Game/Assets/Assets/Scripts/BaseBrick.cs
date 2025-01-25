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
        animIntervalTime = Random.Range(5, 10);
        StartAnim();
    }

    /// <summary>
    /// Helper function to start animation timer. Also called in shine animation.
    /// </summary>
    public void StartAnim() 
    {
        StartCoroutine(PlayAnimation());
    }

    /// <summary>
    /// Coroutine to animate the brick at a random interval
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAnimation()
    {
        animator.SetBool("Play", false);
        yield return new WaitForSeconds(animIntervalTime);
        animator.SetBool("Play", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") == true)
        {
            Destroy(gameObject);
        }
    }
}
