using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetterCodeComp : MonoBehaviour
{
    public CodeComputerManager ManagerScript;
    public string LetterToAdd = "1";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddLetter()
    {
        ManagerScript.CodeToJoin += LetterToAdd;
    }
}
