using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class Vibration : MonoBehaviour
{
    public EasyHand Hand;
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
        EasyInputs.Vibration(Hand, 0.15f, 0.15f);
    }
}
