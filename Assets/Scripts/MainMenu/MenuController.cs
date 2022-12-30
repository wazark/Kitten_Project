using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private GlobalAudioController _globalAudioController;

    [Header("GameObjects")]
    public GameObject gameTitulo;    
    public GameObject buttons;
    public GameObject optionsUI;
    
    [Header("Text")]
    public Text txtPressAnyButton;

    [Header("Cooldown")]
    public float cooldownTitle;

    [Header("Cooldown")]
    public int musicIndexMainMenu;


    private void Start()
    {
        _globalAudioController = FindObjectOfType(typeof(GlobalAudioController)) as GlobalAudioController;
    }

    void Update()
    {
        pressAnyButtonToStart();
    }

    public void pressAnyButtonToStart()
    {
        if(Input.anyKeyDown == true)
        {
            txtPressAnyButton.enabled = false;
            StartCoroutine("cooldownToShowTitle", cooldownTitle);           
        }
    }

    IEnumerator cooldownToShowTitle(float cdTitle)
    {
        yield return new WaitForSeconds(cdTitle);
        gameTitulo.SetActive(true);
        _globalAudioController.playGameMusic(musicIndexMainMenu);
    }

    public void showOptions(bool isOptionVisible)
    {
        if (isOptionVisible == true)
        {
            optionsUI.SetActive(true);
        }
        else
            optionsUI.SetActive(false);
    }

    public void showButtons(bool isButtonsVisible)
    {
        if (isButtonsVisible == true)
        {
            buttons.SetActive(true);
        }
        else
            buttons.SetActive(false);
    }
       
}
