using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenFix : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
