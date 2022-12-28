using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator transitionControllerAnimator;
    private int idScene;
    
    public void StartFade(int sceneIndex)
    {
        idScene = sceneIndex;
        transitionControllerAnimator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(idScene);
    }
}
