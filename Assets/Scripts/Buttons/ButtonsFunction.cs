using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsFunction : MonoBehaviour
    
{
    private MenuController _menuController;

    private void Start()
    {
        _menuController=FindObjectOfType(typeof(MenuController)) as MenuController;
    }
    public void StartGame()
    {
        
    }
    public void Options()
    {
        _menuController.showOptions(true);
        _menuController.showButtons(false);
    }
    public void QuitGame()
    {

    }
}
