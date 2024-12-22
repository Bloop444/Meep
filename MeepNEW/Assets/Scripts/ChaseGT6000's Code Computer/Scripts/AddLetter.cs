using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetterCode : MonoBehaviour
{
    public CodeManager ManagerScript;
    public string LetterToAdd;
    public string HandTag = "HandTag";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(HandTag)) 
        {
            ManagerScript.CodeVar += LetterToAdd;
        }
    }
}
