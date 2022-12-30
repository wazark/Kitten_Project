using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePosition : MonoBehaviour
{
    public Animator titleAnimator;
    public Vector3 defaultScale;
    
        
    void Start()
    {
        StartCoroutine("activeTrigger"); 
    }
    IEnumerator activeTrigger()
    {
        if (this.gameObject.transform.localScale == defaultScale)
        {
            titleAnimator.SetTrigger("ActiveTitlePosition");
        }
        yield return new WaitForSecondsRealtime(0.2f);
        StartCoroutine("activeTrigger");
    }
}
