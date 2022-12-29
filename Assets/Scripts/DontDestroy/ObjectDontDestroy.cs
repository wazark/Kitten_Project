using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDontDestroy : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
        
}
