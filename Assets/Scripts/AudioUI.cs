using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol01", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol01", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol01", 1f);

        AudioManager.instance?.SetMusicVolume(masterSlider.value);
        AudioManager.instance?.SetMusicVolume(musicSlider.value);
        AudioManager.instance?.SetSFXVolume(sfxSlider.value);

        masterSlider.onValueChanged.AddListener(v => {
            AudioManager.instance?.SetMasterVolume(v);
            PlayerPrefs.SetFloat("MasterVol01", v);
        });
        musicSlider.onValueChanged.AddListener(v => {
            AudioManager.instance?.SetMusicVolume(v);
            PlayerPrefs.SetFloat("MusicVol01", v);
        });
        sfxSlider.onValueChanged.AddListener(v => {
            AudioManager.instance?.SetSFXVolume(v);
            PlayerPrefs.SetFloat("SFXVol01", v);
        });
    }
}
