using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    private Animator animator;
    private float animIntervalTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animIntervalTime = Random.Range(3f, 10f);
        StartCoroutine(StartTimer());

        // Random chance to sparkle on start
        if (Random.Range(0, 20) == 0) 
        {
            animator.Play($"Base Layer.Sparkle Pattern {Random.Range(1, 3)}", 0, 0.25f);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(animIntervalTime);
        animator.Play($"Base Layer.Sparkle Pattern {Random.Range(1, 3)}", 0, 0.25f);
        StartCoroutine(StartTimer());
    }
}
