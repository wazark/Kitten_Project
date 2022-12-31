using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunction : MonoBehaviour

{
    private MenuController _menuController;
    private AudioController _audioController;
    private GlobalAudioController _globalAudioController;

    [Header("Start Music Game")]
    public int startGameMusic;

    private void Start()
    {
        _menuController = FindObjectOfType(typeof(MenuController)) as MenuController;
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _globalAudioController = FindObjectOfType(typeof(GlobalAudioController)) as GlobalAudioController;
    }

    public void startGame()
    {
        SceneManager.LoadScene(3);
        _globalAudioController.StartCoroutine("changeMusic", _globalAudioController.allGameMusic[startGameMusic]);
    }

    public void options()
    {
        _menuController.showOptions(true);
        _menuController.showButtons(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void confirm()
    {
        _audioController.buttonConfirm();
        _menuController.showOptions(false);
        _menuController.showButtons(true);
    }

    public void cancel()
    {
        _audioController.buttonCancel();
        _menuController.showOptions(false);
        _menuController.showButtons(true);
    }
    
}
