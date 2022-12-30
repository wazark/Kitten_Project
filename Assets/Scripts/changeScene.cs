using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private FadeTransition _fadeTransition;
    public string sceneName;
    void Start()
    {
        _fadeTransition=FindObjectOfType(typeof(FadeTransition)) as FadeTransition;
        StartCoroutine("loadNewScene");
    }
    
    IEnumerator loadNewScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(sceneName);
    }
}
