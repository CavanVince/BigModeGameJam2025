using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelManager : MonoBehaviour
{
    public static BasicLevelManager Instance;

    public Transform brickParent;

    private void Start()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Determine if the player destroyed all of the bricks
    /// </summary>
    public void CheckPlayerWon() 
    {
        if (brickParent.childCount - 1 == 0) 
        {
            Debug.Log("You Win!");
        }
    }
}
