using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider[] slideVolume;
    public AudioSource sfxSource;
    public AudioSource musicSource;
   
    
    void Start()
    {
        print(PlayerPrefs.GetFloat("valor do boot " + "firstBoot"));
        print(PlayerPrefs.GetFloat("volume musica " + "musicVolume"));
        print(PlayerPrefs.GetFloat("volume SFX " + "sfxVolume"));


        if (PlayerPrefs.GetInt("firstBoot") == 0)
        {
            print(PlayerPrefs.GetFloat("volume musica " + "musicVolume"));
            print(PlayerPrefs.GetFloat("volume SFX " + "sfxVolume"));
            PlayerPrefs.GetFloat("musicVolume", 0.2f);
            PlayerPrefs.GetFloat("sfxVolume", 0.3f);
            PlayerPrefs.GetInt("firstBoot", 1);
        }

        //Current Volume Variable
        float mVolume = PlayerPrefs.GetFloat("musicVolume");
        float sVolume = PlayerPrefs.GetFloat("sfxVolume");

        //Audio Sources

        sfxSource.volume = mVolume;
        slideVolume[0].value = mVolume;

        musicSource.volume = sVolume;
        slideVolume[1].value = sVolume;            
    }
        
    void Update()
    {
        
    }
    public void sfxVolume()
    {
        sfxSource.volume = slideVolume[0].value;
    }

    public void musicVolume()
    {
        musicSource.volume = slideVolume[1].value;
    }
    
    public void ButtonOk()
    {
        PlayerPrefs.SetFloat("sfxVolume", slideVolume[0].value);
        PlayerPrefs.SetFloat("musicVolume", slideVolume[1].value);        
    }
}
