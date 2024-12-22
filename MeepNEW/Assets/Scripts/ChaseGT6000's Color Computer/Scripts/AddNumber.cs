using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNumberColor : MonoBehaviour
{
    public ColorManager ManagerSctipt;
    public string HandTag = "HandTag";
    public float NumberToSet;
    [Header("Select These Below Based On What Color Your Setting!")]
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(HandTag))
        {
            if(Red)
            {
                ManagerSctipt.RedVar = NumberToSet;
            }
            if(Green) 
            { 
                ManagerSctipt.GreenVar = NumberToSet;
            }
            if(Blue) 
            { 
                ManagerSctipt.BlueVar = NumberToSet;
            }
        }
    }
}
