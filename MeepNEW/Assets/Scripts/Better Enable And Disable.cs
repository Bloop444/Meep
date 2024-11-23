using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterEnableAndDisable : MonoBehaviour
{
    public string HandTag = "HandTag";
    public GameObject[] ObjectsToEnable;
    public GameObject[] ObjectsToDisable;
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
            foreach(GameObject obj in ObjectsToEnable) 
            {
                obj.SetActive(true);
            }
            foreach(GameObject obj in ObjectsToDisable)
            { 
                obj.SetActive(false);
            }
        }
    }
}
