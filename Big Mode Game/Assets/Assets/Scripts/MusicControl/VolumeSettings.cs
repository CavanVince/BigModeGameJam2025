using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("soundVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            LoadVolume();
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value;
        SaveVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", slider.value); 
    }

    private void LoadVolume()
    {
        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("soundVolume");
        }
        else if (slider == null && PlayerPrefs.HasKey("soundVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("soundVolume");
        }
        else
        {
            //Do nothing 
        }
    }
}
