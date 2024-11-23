using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class JumpscareGun : MonoBehaviour
{
    public JumpscareManager ManagerScript;
    public string CollideTag;
    public Transform GunTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EasyInputs.GetTriggerButtonDown(ManagerScript.Hand))
        {
            if(Physics.Raycast(GunTip.position, GunTip.forward, out RaycastHit HitInfo, Mathf.Infinity))
            {
                if(HitInfo.collider.CompareTag(CollideTag))
                {
                    JumpscareManager ManagerScript = HitInfo.collider.gameObject.GetComponent<JumpscareManager>();
                    ManagerScript.jumpscaring = true;
                }
            }
        }
    }
}
