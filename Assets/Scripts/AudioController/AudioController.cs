using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider slideMusicVolume;
    public AudioSource sourceMusic;

    void Start()
    {
        if(PlayerPrefs.GetInt("Initialize") == 0)
        {
            PlayerPrefs.GetFloat("musicVolume", 0.3f);
            PlayerPrefs.GetInt("Initialize", 1);
        }

        float mVolume = PlayerPrefs.GetFloat("musicVolume");

        sourceMusic.volume = mVolume;
        slideMusicVolume.value = mVolume;
            
    }
        
    void Update()
    {
        
    }

    public void SetMusicVol()
    {
        sourceMusic.volume = slideMusicVolume.value;
    }

    public void ButtonOk()
    {
        PlayerPrefs.SetFloat("musicVolume", slideMusicVolume.value);
    }
}
