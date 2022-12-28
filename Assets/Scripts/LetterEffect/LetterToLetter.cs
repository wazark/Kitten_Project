using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterToLetter : MonoBehaviour
{
    public Text textLetter;
    public float delayType;
    public string frase;

    void Start()
    {
        textLetter.text=frase;
        StartCoroutine("typeLetter", frase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator typeLetter(string txt)
    {
        for(int letra=0; letra < txt.Length; letra++)
        {
            print(txt[letra]);
            yield return new WaitForSeconds(delayType);
        }
    }
}
