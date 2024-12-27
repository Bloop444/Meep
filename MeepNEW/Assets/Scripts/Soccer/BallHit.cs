using UnityEngine;
using Photon.Pun;

public class BallHit : MonoBehaviourPun
{
    public string handTag = "HandTag";
    public float hitForce = 10f;
    public float upwardForce = 5f;
    public AudioClip hitSound;
    public PhotonView beachballView;

    private AudioSource audioSource;
    private bool hasOwnerShipTransferred = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            if (!hasOwnerShipTransferred)
            {
                if (beachballView.Owner != PhotonNetwork.LocalPlayer)
                {
                    TransferOwnerShipOfBall();
                }
                else if (beachballView.Owner == PhotonNetwork.LocalPlayer)
                {
                    hasOwnerShipTransferred = true;
                }

                if (hasOwnerShipTransferred)
                {
                    Vector3 direction = transform.position - other.transform.position;

                    Vector3 force = direction.normalized * hitForce + Vector3.up * upwardForce;

                    GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

                    if (hitSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(hitSound);
                    }
                    hasOwnerShipTransferred = false;
                }
            }
        }
    }

    private void TransferOwnerShipOfBall()
    {
        beachballView.TransferOwnership(PhotonNetwork.LocalPlayer);
        hasOwnerShipTransferred = true;
    }
}
