using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=yWCHaTwVblk&ab_channel=Hooson

    [SerializeField] Slider volumeSlider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolumeSettings();
        }
        else
        {
            LoadVolumeSettings(); 
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
