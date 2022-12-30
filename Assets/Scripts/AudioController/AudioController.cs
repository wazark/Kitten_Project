using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public GlobalAudioController _globalAudioController;

    [Header("Audio Settings")]
    public Slider musicSlider;    
    public Slider sfxSlider;    
    


    void Start()
    {
        _globalAudioController = FindObjectOfType(typeof(GlobalAudioController)) as GlobalAudioController;        
    }        
  
    public void sfxVolume()
    {
        _globalAudioController.sfxSource.volume = sfxSlider.value;
    }

    public void musicVolume()
    {
        _globalAudioController.musicSource.volume = musicSlider.value;
    }
    
    public void buttonConfirm()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value); 
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void buttonCancel()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
