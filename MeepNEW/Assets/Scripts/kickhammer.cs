using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;

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
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}