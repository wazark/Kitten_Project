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
    public float fadeMusicVolume;

    [Header("Sound Effect Clips")]
    public AudioClip[] allGameSounds;

    private float currentMusicVolume;
    

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

    public void playGameMusic(int musicIndex, bool musicLoop)
    {
        musicSource.clip = allGameMusic[musicIndex];
        musicSource.Play();
        musicSource.loop = musicLoop;
    }

    public IEnumerator changeMusic(AudioClip clip)
    {
        currentMusicVolume  = musicSource.volume;

        for(float volM = currentMusicVolume; volM > 0; volM -= fadeMusicVolume)
        {
            musicSource.volume = volM;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = clip;
        musicSource.Play();

        for( float volM = 0; volM < currentMusicVolume; volM += fadeMusicVolume )
        {
            musicSource.volume = volM;
            yield return new WaitForEndOfFrame();
        }
    }
   
}
