using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterToLetter : MonoBehaviour
{
    public Text textLetter;
    public string frase;

    void Start()
    {
        textLetter.text=frase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
