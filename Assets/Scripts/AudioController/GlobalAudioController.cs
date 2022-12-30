using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip[] allGameMusic;

    [Header("Sound Effect Clips")]
    public AudioClip[] allGameSounds;

    private float currentMusicVolume;
    private float currentSoundVolume;

    void Start()
    {
        firstBoot();

    }
       
    void Update()
    {
        
    }
      
    public void firstBoot()
    {
        if (PlayerPrefs.GetInt("boot") == 0)
        {
            PlayerPrefs.SetFloat("musicVolume", 0.2f);
            PlayerPrefs.SetFloat("sfxVolume", 0.3f);
            PlayerPrefs.SetInt("boot", 1);            
        }
                
        //Set the last volume save by player
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume");
                
    }

    public void playGameMusic(int musicIndex)
    {
        musicSource.clip = allGameMusic[musicIndex];
        musicSource.Play();
        musicSource.loop = true;
    }
   
}
