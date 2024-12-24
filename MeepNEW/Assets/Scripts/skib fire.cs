using easyInputs;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class KeosSkibidyFlameThingy : MonoBehaviour
{
    [Header("Photon Sync")]
    public PhotonView PTView;
    [Header("Trigger")]
    public EasyHand TriggerHand;
    [Header("Flame")]
    public ParticleSystem Flame;
    public AudioSource Sound;
    [Header("Enable Disable when shooting")]
    public GameObject[] Enable;
    public GameObject[] Disable;

    [Header("Test")]
    public bool Test;

    //Priv Stuff

    private bool IsFlaming;

    private void Update()
    {
        if (!Test && PTView.IsMine && EasyInputs.GetTriggerButtonDown(TriggerHand)
         || Test && PTView.IsMine && Input.GetMouseButton(0) && !IsFlaming)
        {
            PTView.RPC(nameof(Flaming), RpcTarget.AllBuffered);
        }
        else if (!Test && PTView.IsMine && !EasyInputs.GetTriggerButtonDown(TriggerHand) && IsFlaming
            || Test && PTView.IsMine && !Input.GetMouseButton(0) && IsFlaming)
        {
            PTView.RPC(nameof(StopFlaming), RpcTarget.AllBuffered);
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    public void Flaming()
    {
        IsFlaming = true;
        ED(Enable, true);
        ED(Disable, false);
        Flame.Play();
        Sound.Play();
    }

    [PunRPC]
    public void StopFlaming()
    {
        IsFlaming = false;
        ED(Enable, false);
        ED(Disable, true);
        Flame.Stop();
        Sound.Stop();
    }

    public void ED(GameObject[] SkibidyArray, bool State)
    {
        foreach(GameObject g in SkibidyArray)
        {
            g.SetActive(State);
        }
    }
}
//                                                                                          Creators:
//if you are reading this message it means that you are editing it please leave my not here - Keo.CS, 
