using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterToLetter : MonoBehaviour
{
    public Text textLetter;
    public float delayType;
    public float wordDelay;
    public float fraseDelay;
    public string[] frases;
    private int idFrase;
    

    void Start()
    {        
        StartCoroutine("typeLetter");
    }   
    IEnumerator typeLetter()
    {        
        for(idFrase = 0; idFrase < frases.Length; idFrase++) {
            textLetter.text = "";

            for (int letra=0; letra < frases[idFrase].Length; letra++)
            {
            textLetter.text += frases[idFrase][letra];
                if (frases[idFrase][letra] == ' ') // utiliza o apóstrolo em vez da aspa pois quando utilizamos aspa nos referimos a String e quando falamos de uma letra específica ela é vista como um character, por isto tem que usar apóstrofo. 
                {
                yield return new WaitForSeconds(wordDelay);
                }
                yield return new WaitForSeconds(delayType);

            }
            yield return new WaitForSeconds(fraseDelay);

        }
    }
}
