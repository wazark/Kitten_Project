using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider musicSlider;
    public AudioSource musicSource;

    public Slider sfxSlider;
    public AudioSource sfxSource;

    public AudioClip[] gameMusics;


    void Start()
    {
        if(PlayerPrefs.GetInt("firstBoot") == 0)
        {
            PlayerPrefs.SetFloat("musicVol", 0.5f);
            PlayerPrefs.SetFloat("sfxVol", 0.3f);
            PlayerPrefs.SetInt("firstBoot", 1);
        }

        float mVolume = PlayerPrefs.GetFloat("musicVol");
        float sVolume = PlayerPrefs.GetFloat("sfxVol");

        musicSlider.value = mVolume;
        musicSource.volume = mVolume;

        sfxSlider.value = sVolume;
        sfxSource.volume = sVolume;
    }        
  
    public void sfxVolume()
    {
        sfxSource.volume = sfxSlider.value;
    }

    public void musicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    
    public void buttonConfirm()
    {
        PlayerPrefs.SetFloat("musicVol" , musicSlider.value); 
        PlayerPrefs.SetFloat("sfxVol" , sfxSlider.value);
    }

    public void buttonCancel()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVol");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
    }
}
