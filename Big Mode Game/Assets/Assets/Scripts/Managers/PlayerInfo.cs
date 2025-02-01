using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    [SerializeField]
    List<Image> trinketImages;
    private Vector3 origTrinketImageScale;

    /// <summary>
    /// The player's trinkets
    /// </summary>
    public List<TrinketParent> PlayerTrinkets { get; private set; } = new List<TrinketParent>{null, null, null, null, null};

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

        origTrinketImageScale = trinketImages[0].transform.localScale;
    }

    /// <summary>
    /// Adds the given trinket to the player's trinket list, if there is room.
    /// </summary>
    /// <param name="trinketToAdd">Trinket being added</param>
    /// <returns>Was the trinket successfully added</returns>
    public bool AddTrinket(ShopTrinketScriptableObject trinketToAdd)
    {
        for (int i = 0; i < PlayerTrinkets.Count; i++)
        {
            if (PlayerTrinkets[i] == null)
            {
                // Add a new instance of the trinket to the player's inventory
                switch (trinketToAdd.trinketType)
                {
                    case TrinketType.BIGMODE:
                        PlayerTrinkets[i] = new BigMode();
                        break;
                    case TrinketType.BLUEBRICKBUFF:
                        PlayerTrinkets[i] = new BlueBrickBuff();
                        break;
                    case TrinketType.DAGGER:
                        PlayerTrinkets[i] = new Dagger();
                        break;
                    case TrinketType.GREASEBALL:
                        PlayerTrinkets[i] = new Greaseball();
                        break;
                    case TrinketType.GREENBRICKBUFF:
                        PlayerTrinkets[i] = new GreenBrickBuff();
                        break;
                    case TrinketType.HEALTHPOTION:
                        PlayerTrinkets[i] = new HealthPotion();
                        break;
                    case TrinketType.REDBRICKBUFF:
                        PlayerTrinkets[i] = new RedBrickBuff();
                        break;
                    case TrinketType.SHOTGUN:
                        PlayerTrinkets[i] = new Shotgun();
                        break;
                    case TrinketType.SIRBOUNCEALOT:
                        PlayerTrinkets[i] = new SirBounceAlot();
                        break;
                    case TrinketType.SMALLMODE:
                        PlayerTrinkets[i] = new SmallMode();
                        break;

                    default:
                        Debug.Log("ERROR: TRINKET TYPE NOT IN PLAYER INFO!");
                        break;
                }

                trinketImages[i].sprite = trinketToAdd.trinketSprite;
                trinketImages[i].GetComponent<PlayerTrinkets>().trinket = PlayerTrinkets[i];
                ZoomInTrinket(trinketImages[i].transform);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Helper function to zoom in the trinket
    /// </summary>
    private void ZoomInTrinket(Transform trinketTransform) 
    {
        trinketTransform.localScale = Vector3.zero;
        trinketTransform.gameObject.SetActive(true);
        trinketTransform.DOScale(origTrinketImageScale, 0.5f).SetEase(Ease.Linear);
    }

    public void ZoomOutTrinket(Transform trinketTransform) 
    {
        trinketTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(() => 
        {
            trinketTransform.gameObject.SetActive(false);
            trinketTransform.localScale = origTrinketImageScale;
        });
    }

}
