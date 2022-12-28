using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator transitionControllerAnimator;
    public int idScene;
    
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            StartFade(idScene);
        }
        
    }

    public void StartFade(int sceneIndex)
    {
        transitionControllerAnimator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(idScene);
    }
}
