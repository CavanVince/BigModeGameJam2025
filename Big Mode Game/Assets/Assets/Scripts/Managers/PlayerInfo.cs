using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    /// <summary>
    /// The player's trinkets
    /// </summary>
    public List<TrinketParent> PlayerTrinkets { get; private set; } = new List<TrinketParent>(5);

    /// <summary>
    /// The player's money
    /// </summary>
    public int PlayerMoney { get; set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds the given trinket to the player's trinket list, if there is room.
    /// </summary>
    /// <param name="trinketToAdd">Trinket being added</param>
    /// <returns>Was the trinket successfully added</returns>
    public bool AddTrinket(TrinketParent trinketToAdd) 
    {
        for (int i = 0; i < PlayerTrinkets.Count; i++) 
        {
            if (PlayerTrinkets[i] == null) 
            {
                PlayerTrinkets[i] = trinketToAdd;
                return true;
            }
        }
        return false;
    }

}
