using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Remove the trinket from the player's inventory
        PlayerInfo.Instance.PlayerTrinkets[PlayerInfo.Instance.PlayerTrinkets.IndexOf(transform.parent.GetComponent<PlayerTrinkets>().trinket)] = null; // Don't judge me it's 4am and I'm all alone :(
        transform.parent.GetComponent<PlayerTrinkets>().trinket = null;
        PlayerInfo.Instance.ZoomOutTrinket(transform.parent);
        gameObject.SetActive(false);
    }
}
