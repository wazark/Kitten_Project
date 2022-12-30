using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject gameTitulo;
    public Text txtPressAnyButton;
    public float cooldownTitle;
    public GameObject buttonsMainMenu;

    
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
            buttonsMainMenu.SetActive(true);
        }
    }
    IEnumerator cooldownToShowTitle(float cdTitle)
    {
        yield return new WaitForSeconds(cdTitle);
        gameTitulo.SetActive(true);
    }
}
