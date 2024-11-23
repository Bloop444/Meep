using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class Flashlight : MonoBehaviour
{
    public GameObject[] FlashlightLightGameObjects;
    public EasyHand Hand = EasyHand.RightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EasyInputs.GetSecondaryButtonDown(Hand))
        {
            foreach(GameObject obj in FlashlightLightGameObjects) 
            { 
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
