using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    public NameScript nameScript;
    public string Letter;

    public void StartAddLetter()
    { 
        nameScript.NameVar += Letter;
    }
}
