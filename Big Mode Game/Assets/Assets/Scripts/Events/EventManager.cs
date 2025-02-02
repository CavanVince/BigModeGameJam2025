using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [SerializeField]
    private OptionSelect YesOption;

    [SerializeField]
    private OptionSelect NoOption;

    [SerializeField]
    List<MapEvent> PotentialEvents;

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
        }
    }

    public void SelectResult(bool isConfirm)
    {
        if(isConfirm) 
        {
            if(SelectedEvent != null && CanSelect)
            {
                Text.text = SelectedEvent.ReturnConfirmedResult();
                CanSelect = false;
            }
        }
        else if(!isConfirm)
        {
            if (SelectedEvent != null && CanSelect)
            {
                Text.text = SelectedEvent.ReturnRejectedResult();
                CanSelect= false;
            }
        }
    }
}
