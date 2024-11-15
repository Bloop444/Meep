using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorVar : MonoBehaviour
{
    public ColorManager ManagerScript;
    public float NumberToSet;
    public bool Red;
    public bool Green;
    public bool Blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartSetColor()
    {
        if(Red == true)
        {
            ManagerScript.Red = NumberToSet;
        }
        else if(Green == true)
        {
            ManagerScript.Green = NumberToSet;
        }
        else if(Blue == true)
        {
            ManagerScript.Blue = NumberToSet;
        }
    }
}
