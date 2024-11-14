using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverRay : MonoBehaviour
{
    public XRInteractorLineVisual LineVisual;
    public string UITag = "UI";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit HitInfo, Mathf.Infinity))
        {
            if (HitInfo.collider.CompareTag(UITag))
            {
                LineVisual.enabled = true;
            }
            else
            {
                LineVisual.enabled = false;
            }
        }
    }
}
