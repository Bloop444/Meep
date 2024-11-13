using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RocketLeagueManager : MonoBehaviour
{
    [Header("You Should Add A Photon View To This And The \nTMPro!")]
    public TextMeshPro ScoreDisplay;
    public float RedScore;
    public float BlueScore;
    public Transform BallTransform;
    public Transform RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        RedScore = 0;
        BlueScore = 0;
        ScoreDisplay.text = "0 - 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.InRoom == false)
        {
            ScoreDisplay.text = "0 - 0";
        }
        else
        {
            ScoreDisplay.text = RedScore + "-" + BlueScore;
        }
    }
    public void ResetPosition()
    {
        BallTransform.position = RespawnPoint.position;
    }
}
