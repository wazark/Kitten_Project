using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterToLetter : MonoBehaviour
{
    public Text textLetter;
    public float delayType;
    public float wordDelay;
    public string frase;

    void Start()
    {
        textLetter.text=frase;
        StartCoroutine("typeLetter", frase);
    }   
    IEnumerator typeLetter(string txt)
    {
        textLetter.text = "";

        for(int letra=0; letra < txt.Length; letra++)
        {
            textLetter.text += txt[letra];
            if (txt[letra] == ' ') // utiliza o apóstrolo em vez da aspa pois quando utilizamos aspa nos referimos
                                   // a String e quando falamos de uma letra específica ela é vista como um character,
                                   // por isto tem que usar apóstrofo. 
            {
                yield return new WaitForSeconds(wordDelay);
            }

            yield return new WaitForSeconds(delayType);
        }
    }
}
