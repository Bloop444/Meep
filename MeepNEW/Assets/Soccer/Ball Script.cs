using UnityEngine;
using Photon.Pun;

public class BallScript : MonoBehaviourPun
{
    public SoccerGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedGoal"))
        {
            photonView.RPC("GoalScoredRPC", RpcTarget.All, "Red");
        }
        else if (other.CompareTag("BlueGoal"))
        {
            photonView.RPC("GoalScoredRPC", RpcTarget.All, "Blue");
        }
    }

    [PunRPC]
    public void GoalScoredRPC(string team)
    {
        gameManager.GoalScored(team);
    }
}
