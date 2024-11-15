using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    public GameObject[] PreviousMonitors;
    public GameObject[] NextMonitors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextTabs()
    {
        foreach (GameObject obj in PreviousMonitors)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in NextMonitors)
        {
            obj.SetActive(true);
        }
    }
}
