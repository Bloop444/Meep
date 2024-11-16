using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab.MultiplayerModels;

public class Bullet : MonoBehaviour
{
    [Header("Whoever reads this tell ben hes not the sigma")]

    public GameObject bullet;
    public int damageAmount = 10;
    public string DamageTag;
    public PhotonView Photonview;

    public void Start()
    {
        GetComponent<Bullet>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Photonview.IsMine);
        {
            return;
        }
        if (other.CompareTag(DamageTag))
        {
             Health health = other.GetComponent<Health>();
             if (health != null)
             {
                 health.TakeDamage(damageAmount);
             }
        }

        Destroy(bullet);
    }
}