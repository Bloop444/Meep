using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CodeComputerManager : MonoBehaviour
{
    public TextMeshProUGUI DisplayText;
    public string CodeToJoin;
    public bool TestPress;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(CodeToJoin.Length > 11)
        {
            CodeToJoin = CodeToJoin.Remove(CodeToJoin.Length - 1);
        }
        DisplayText.text = "Join Room: " + CodeToJoin;
        if (TestPress == true && PhotonNetwork.CurrentRoom.Name != CodeToJoin && PhotonNetwork.IsConnected && PhotonNetwork.IsConnectedAndReady) 
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 10, 
                IsVisible = true,        
                IsOpen = true            
            };

            PhotonNetwork.JoinOrCreateRoom(CodeToJoin, options, TypedLobby.Default);
        }
    }
}
