using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a little cringe but basically there is an enum for each trinket. Don't hate the player, hate the game >:)
/// </summary>
public enum TrinketType {
    BIGMODE,
    BLUEBRICKBUFF,
    DAGGER,
    GREASEBALL,
    GREENBRICKBUFF,
    HEALTHPOTION,
    REDBRICKBUFF,
    SHOTGUN,
    SIRBOUNCEALOT,
    SMALLMODE,
}

[CreateAssetMenu(fileName ="New Shop Trinket", menuName ="Trinkets")]
public class ShopTrinketScriptableObject : ScriptableObject
{
    public Sprite trinketSprite;
    public int trinketPrice;
    public TrinketType trinketType;
    public string trinketDescription;
}
