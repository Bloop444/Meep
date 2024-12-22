using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CodeManager : MonoBehaviour
{
    public TextMeshPro DisplayText;
    public string CodeVar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = "Code: " + CodeVar;
    }
    internal void JoinRoom()
    {
        RoomOptions roomOption = new RoomOptions
        {
            MaxPlayers = 10,
            IsVisible = true,
            IsOpen = true,
        };
        PhotonNetwork.JoinOrCreateRoom(CodeVar, roomOption, TypedLobby.Default);
    }
}
