using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SoccerGameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Variables
    public int redTeamScore = 0;
    public int blueTeamScore = 0;
    public TMP_Text redTeamScoreText;
    public TMP_Text blueTeamScoreText;
    public TMP_Text winnerText;
    public GameObject soccerBall;
    public Transform ballRespawnPoint;
    public int pointsToWin = 5;

    public Collider redGoalTrigger;
    public Collider blueGoalTrigger;
    #endregion

    #region Initialization
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ResetBall();
        }
    }
    #endregion

    #region Ball Handling
    private void ResetBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            soccerBall.transform.position = ballRespawnPoint.position;
            soccerBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    #endregion

    #region Goal Detection
    private void OnTriggerEnter(Collider other)
    {
        if (other == redGoalTrigger)
        {
            GoalScored("Blue");
        }
        else if (other == blueGoalTrigger)
        {
            GoalScored("Red");
        }
    }
    #endregion

    #region Score Handling
    public void GoalScored(string team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (team == "Red")
            {
                redTeamScore++;
                redTeamScoreText.text = redTeamScore.ToString();
            }
            else if (team == "Blue")
            {
                blueTeamScore++;
                blueTeamScoreText.text = blueTeamScore.ToString();
            }

            if (redTeamScore >= pointsToWin)
            {
                Winner("Red Team", Color.red);
            }
            else if (blueTeamScore >= pointsToWin)
            {
                Winner("Blue Team", Color.blue);
            }
            else
            {
                ResetBall();
            }
        }
    }
    #endregion

    #region Winner Handling
    private void Winner(string winningTeam, Color winnerColor)
    {
        winnerText.text = "Winner: " + winningTeam;
        winnerText.color = winnerColor;
        PhotonNetwork.RaiseEvent(100, winningTeam, new Photon.Realtime.RaiseEventOptions { Receivers = Photon.Realtime.ReceiverGroup.All }, ExitGames.Client.Photon.SendOptions.SendReliable);
        Invoke("ResetGame", 3f);
    }
    #endregion

    #region Game Reset Handling
    private void ResetGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            redTeamScore = 0;
            blueTeamScore = 0;
            redTeamScoreText.text = redTeamScore.ToString();
            blueTeamScoreText.text = blueTeamScore.ToString();
            winnerText.text = "";
            winnerText.color = Color.white;
            ResetBall();
        }
    }
    #endregion

    #region Synchronization
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(redTeamScore);
            stream.SendNext(blueTeamScore);
        }
        else
        {
            redTeamScore = (int)stream.ReceiveNext();
            blueTeamScore = (int)stream.ReceiveNext();
        }
    }
    #endregion
}
