using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrinkets : MonoBehaviour
{
    public TrinketParent trinket;

    private void OnMouseDown()
    {
        if (ShopManager.Instance.InShop && trinket != null) 
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
