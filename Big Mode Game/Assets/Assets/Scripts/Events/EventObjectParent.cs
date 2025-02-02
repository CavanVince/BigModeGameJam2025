using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectParent : MonoBehaviour
{
    public string DisplayedText = "I have no event";

    public virtual string ReturnConfirmedResult()
    {
        return DisplayedText;
    }

    public virtual string ReturnRejectedResult()
    {
        return DisplayedText;
    }
}
