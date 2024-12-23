using Photon.Pun;
using UnityEngine;

public class HydraTagHands : MonoBehaviourPun
{
    public string tagHandTag = "HandTag";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerTagging(other);
        }
    }

    private void HandlePlayerTagging(Collider other)
    {
        PhotonView otherPhotonView = other.GetComponent<PhotonView>();
        if (otherPhotonView != null && !otherPhotonView.IsMine)
        {
            otherPhotonView.RPC("TagPlayer", RpcTarget.All);
        }
    }
}
