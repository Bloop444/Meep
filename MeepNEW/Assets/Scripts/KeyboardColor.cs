using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColor : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        rend.sharedMaterial = material[0];
    }

    private void OnTriggerExit(Collider other)
    {
        rend.sharedMaterial = material[1];
    }
}
