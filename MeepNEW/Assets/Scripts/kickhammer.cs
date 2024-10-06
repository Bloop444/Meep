using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class kickhammer : MonoBehaviour
{
    public PhotonView ptView;

    void OnTriggerEnter(Collider other)
    {
        if (ptView.IsMine)
        {
            return;
        }
        else
        {
            Application.Quit();
        }
    }
}