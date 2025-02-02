using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [SerializeField]
    public OptionSelect YesOption;

    [SerializeField]
    public OptionSelect NoOption;

    [SerializeField]
    List<EventObjectParent> PotentialEvents;

    [SerializeField]
    TextMeshProUGUI Text;

    public bool CanSelect = true;

    private EventObjectParent SelectedEvent;

    // Start is called before the first frame update
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GenerateEvent();
    }

    private void GenerateEvent()
    {
        if(PotentialEvents.Count != 0)
        {
            CanSelect = true;
            SelectedEvent = PotentialEvents[Random.Range(0, PotentialEvents.Count)];
            Text.text = SelectedEvent.DisplayedText;
            PotentialEvents.Remove(SelectedEvent);
        }
        //Set case to have only combat encounters if youre out of events somehow

        if (SelectedEvent is CombatEncounter)
        {
            YesOption.gameObject.SetActive(false);
            NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
        }

        if (SelectedEvent is ShopEncounter)
        {
            YesOption.gameObject.SetActive(false);
            NoOption.gameObject.SetActive(false);
            BasicLevelManager.Instance.CanGoToNextScreen = true;
        }
    }

    private void Update()
    {
        if (SelectedEvent is CombatEncounter && Input.GetKeyDown(KeyCode.Space))
        {
            BasicLevelManager.Instance.LoadEnemyLevel();
        }

        if (SelectedEvent is ShopEncounter && Input.GetKeyDown(KeyCode.Space))
        {
            BasicLevelManager.Instance.LoadShop();
        }
    }

    public void SelectResult(bool isConfirm)
    {
        if(isConfirm) 
        {
            if(SelectedEvent != null && CanSelect)
            {
                Text.text = SelectedEvent.ReturnConfirmedResult();
                //CanSelect = false;
                BasicLevelManager.Instance.CanGoToNextScreen = true;
            }
        }
        else if(!isConfirm)
        {
            if (SelectedEvent != null && CanSelect)
            {
                Text.text = SelectedEvent.ReturnRejectedResult();
                CanSelect= false;
                YesOption.gameObject.SetActive(false);
                NoOption.gameObject.SetActive(false);
                BasicLevelManager.Instance.CanGoToNextScreen = true;
            }
        }
    }
}
