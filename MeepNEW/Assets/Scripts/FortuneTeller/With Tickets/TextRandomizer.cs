using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextRandomizer : MonoBehaviour
{
    public TextMeshPro text;
    public List<string> fortunes;

    public void RandomizeText()
    {
        int random = Random.Range(0, fortunes.Count);

        foreach (string s in fortunes)
        {
            text.text = fortunes[random];
        }
    }
}