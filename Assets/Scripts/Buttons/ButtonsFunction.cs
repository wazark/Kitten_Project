using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunction : MonoBehaviour

{
    private MenuController _menuController;
    private AudioController _audioController;

    private void Start()
    {
        _menuController = FindObjectOfType(typeof(MenuController)) as MenuController;
        _audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    public void startGame()
    {
        SceneManager.LoadScene(2);
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
        _audioController.ButtonOk();
        _menuController.showOptions(false);
        _menuController.showButtons(true);
    }

    public void cancel()
    {
        _menuController.showOptions(false);
        _menuController.showButtons(true);
    }
    
}
