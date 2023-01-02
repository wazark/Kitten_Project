using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    private GlobalAudioController _globalAudioController;
    private FadeTransition _fadeTransition;

    [Header("Scene Index to Start")]
    public int startSceneIndex;
    private bool isChecked;
    [Header("Start Music Game")]
    public int startGameMusic;

    void Start()
    {
        _globalAudioController = FindObjectOfType(typeof(GlobalAudioController)) as GlobalAudioController;
        _fadeTransition =FindObjectOfType(typeof(FadeTransition)) as FadeTransition;

        _globalAudioController.musicSource.loop= false;
    }

    
    void Update()
    {
        if(isChecked == false && _globalAudioController.musicSource.isPlaying == false)
        {
            isChecked = true;
            _fadeTransition.StartFade(startSceneIndex);
            _globalAudioController.StartCoroutine("changeMusic", _globalAudioController.allGameMusic[startGameMusic]);
        }
        
        
    }
}
