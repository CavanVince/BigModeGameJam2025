using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for bricks
/// </summary>
public class BrickParent : MonoBehaviour
{
    /// <summary>
    /// The amount of score a brick is worth
    /// </summary>
    [SerializeField]
    int score = 100;

    /// <summary>
    /// The destruction particles
    /// </summary>
    [SerializeField]
    GameObject particles;

    /// <summary>
    /// The material to render on the destruction particles
    /// </summary>
    [SerializeField]
    Material particleMat;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball") != true) return;
        
        // Notify score manager & audio manager that brick was hit
        BasicLevelManager.Instance.AddScore(score);
    }

    /// <summary>
    /// Code to call when brick is destroyed
    /// </summary>
    protected virtual void DestroyBrick() 
    {
        BasicLevelManager.Instance.CheckPlayerWon();

        // Render particles
        if (particles != null) 
        {
            GameObject spawnedParticle = Instantiate(particles, transform.position, Quaternion.identity);
            spawnedParticle.GetComponent<ParticleSystemRenderer>().material = particleMat;
        }

        Destroy(gameObject);
    }
}
