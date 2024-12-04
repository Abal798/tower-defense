using System;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer masterVolumeMixer;
    [SerializeField] private Slider masterVolumeSlider;
    
    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefsManager.PPManager.LoadFloatValue("masterVolume");
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterVolumeSlider.value;
        masterVolumeMixer.SetFloat("master", Mathf.Log10(masterVolumeSlider.value)*20);
        PlayerPrefsManager.PPManager.SaveFloatValue("masterVolume", masterVolumeSlider.value);
    }
}
