using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OfflineRig : MonoBehaviour
{
    public GameObject Rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.InRoom == false)
        {
            Rig.SetActive(true);
        }
        else
        {
            Rig.SetActive(false);
        }
    }
}
