using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backspace : MonoBehaviour
{
    public CodeComputerManager ManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartBackspace()
    {
        ManagerScript.CodeToJoin = ManagerScript.CodeToJoin.Remove(ManagerScript.CodeToJoin.Length - 1);
    }
}
