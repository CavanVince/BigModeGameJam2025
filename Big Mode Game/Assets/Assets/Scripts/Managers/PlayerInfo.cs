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
    public List<TrinketParent> PlayerTrinkets { get; private set; } = new List<TrinketParent> { null, null, null, null, null };

    [SerializeField]
    private RuntimeAnimatorController blackHoleAnim;

    [SerializeField]
    private RuntimeAnimatorController lightningStrikeAnim;

    /// <summary>
    /// The player's money
    /// </summary>
    public int PlayerMoney { get; set; } = 25;

    // The number of balls the player has left
    public int PlayerBallCount { get; set; } = 0;

    // The baseline number of balls the player starts with
    public int StartingBallCount { get; set; } = 5;

    /// <summary>
    /// The minimum multilpier to apply to the score multiplier
    /// </summary>
    public int MinScoreMult { get; set; } = 1;

    void Awake()

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

        PlayerBallCount = StartingBallCount;
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
                    case TrinketType.GHOSTBALL:
                        PlayerTrinkets[i] = new GhostBall();
                        break;
                    case TrinketType.DAGGER:
                        PlayerTrinkets[i] = new Dagger();
                        break;
                    case TrinketType.GREASEBALL:
                        PlayerTrinkets[i] = new Greaseball();
                        break;
                    case TrinketType.HEALTHPOTION:
                        PlayerTrinkets[i] = new HealthPotion();
                        break;
                    case TrinketType.JESTER:
                        PlayerTrinkets[i] = new Jester();
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
                    case TrinketType.BOOSTERROCKET:
                        PlayerTrinkets[i] = new BoosterRocket();
                        break;
                    case TrinketType.SPELLOFGIGANTIFICATION:
                        PlayerTrinkets[i] = new SpellOfGigantification();
                        break;
                    case TrinketType.BLACKHOLE:
                        PlayerTrinkets[i] = new BlackHoleTrinket(blackHoleAnim);
                        break;
                    case TrinketType.LIGHTNING:
                        PlayerTrinkets[i] = new LightningStrikeTrinket(lightningStrikeAnim);
                        break;
                    case TrinketType.MISTAMONEYBAGS:
                        PlayerTrinkets[i] = new MistaMoneybags();
                        break;
                    case TrinketType.LIFETREE:
                        PlayerTrinkets[i] = new LifeTree();
                        break;
                    case TrinketType.WIZARDCAP:
                        PlayerTrinkets[i] = new WizardCap();
                        break;
                    case TrinketType.POGO:
                        PlayerTrinkets[i] = new Pogo();
                        break;
                    case TrinketType.CHAOS:
                        PlayerTrinkets[i] = new Chaos();
                        break;
                    case TrinketType.ARROW:
                        PlayerTrinkets[i] = new Arrow();
                        break;
                    case TrinketType.ELASTICSHOT:
                        PlayerTrinkets[i] = new ElasticShot();
                        break;
                    case TrinketType.SPLITSHOT:
                        PlayerTrinkets[i] = new Arrow();
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
