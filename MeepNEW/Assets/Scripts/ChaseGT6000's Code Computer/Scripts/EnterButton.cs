using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnterButton : MonoBehaviour
{
    public CodeComputerManager ManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartEnter()
    {
        {
            if (PhotonNetwork.IsConnected && ManagerScript.CodeToJoin.Length > 1)
            {
                PhotonNetwork.JoinRoom(ManagerScript.CodeToJoin);
            }
        }
    }
}
