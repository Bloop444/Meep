using Photon.Pun;
using UnityEngine;

public class WaterRipple : MonoBehaviourPun
{
    [Header("This script was made by Keo.cs")]
    public LayerMask targetLayer;
    public GameObject ripplePrefab;
    public float deleteAfterSeconds = 3.0f;

    void OnTriggerEnter(Collider other)
    {
        if ((targetLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Vector3 spawnPosition = transform.position;
            PhotonNetwork.Instantiate(ripplePrefab.name, spawnPosition, Quaternion.identity);
            Invoke("DeleteRipple", deleteAfterSeconds);
        }
    }

    void DeleteRipple()
    {
        if (photonView.IsMine && ripplePrefab != null)
        {
            PhotonNetwork.Destroy(ripplePrefab);
        }
    }
}
