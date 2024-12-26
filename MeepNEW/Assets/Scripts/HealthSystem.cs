using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;
using GorillaLocomotion;

public class HealthSystem : MonoBehaviour
{
    [Header("Your HandTag")]
    public string HandTag;

    [Header("Object with health manager")]
    public HealthSystemManager HealthManager;

    [Header("Your GorillaPlayer")]
    public Transform GorillaPlayer;

    [Header("PhotonView")]
    public PhotonView Ptv;

    [Header("Respawn Point")]
    public Transform RespawnPoint;

    [Header("Damage the object does")]
    public float Damage = 10f;

    [Header("Dead Object")]
    public GameObject DeadMonkey;

    private Player gorillaMovement;
    private LayerMask l;

    void Start()
    {
        GorillaPlayer.gameObject.TryGetComponent<Player>(out gorillaMovement);
        
        l = gorillaMovement.locomotionEnabledLayers;
    }
    
    void OnTriggerEnter(Collider Tag)
    {
        if(Tag.CompareTag(HandTag))
        {
            HealthManager.Health -= Damage;
            HealthManager.HealthText.text = HealthManager.Health.ToString("F2");
            CheckDead();
            
        }
    }

    void CheckDead()
    {
        if(HealthManager.Health <= 0f)
        {
            Ptv.RPC("KillPlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    void KillPlayer()
    {
        GameObject DeadMonkeyClone = Instantiate(DeadMonkey, GorillaPlayer.position, GorillaPlayer.rotation);
        Teleport();
        HealthManager.Health = HealthManager.MaxHealth;
        HealthManager.HealthText.text = HealthManager.Health.ToString("F2");
        Destroy(DeadMonkeyClone, HealthManager.ModelDeadTime);
        
    }

    void Teleport()
    {
        if (gorillaMovement != null)
        {
            gorillaMovement.locomotionEnabledLayers = 0;

            GorillaPlayer.position = RespawnPoint.position;

            GorillaPlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            
            StartCoroutine(Delay(l));
        }
    }

    IEnumerator Delay(LayerMask originaLayers)   
    {
        yield return new WaitForSeconds(0.1f);
        
        gorillaMovement.locomotionEnabledLayers = l;
    }
    
    
   
}
