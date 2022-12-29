using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour
{
    public FadeTransition _fadeTransition;
    void Start()
    {
        _fadeTransition=FindObjectOfType(typeof(FadeTransition)) as FadeTransition;
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            _fadeTransition.StartFade(1);
            _fadeTransition.OnFadeComplete();
        }
    }
}
