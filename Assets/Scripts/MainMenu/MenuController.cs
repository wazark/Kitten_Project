using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject gameTitulo;
    public GameObject musicSource;
    public GameObject optionsUI;
    public GameObject buttons;
    public Text txtPressAnyButton;
    public float cooldownTitle;
    

    
    void Update()
    {
        PressAnyButtonToStart();
    }

    public void PressAnyButtonToStart()
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
        musicSource.SetActive(true);
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
