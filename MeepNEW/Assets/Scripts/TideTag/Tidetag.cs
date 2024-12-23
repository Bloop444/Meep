using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using GorillaLocomotion;

public class HydraTag : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Bloop I want you")]
    public float tagTime = 10f;
    public float endRoundTime = 5f;
    public int minPlayersToStart = 2;
    public AudioSource tagSound;
    public AudioSource endRoundSound;
    public SkinnedMeshRenderer playerRenderer;
    public Material taggedMaterial;
    public Material untaggedMaterial;

    private bool isTagged = false;
    private float tagCooldown = 0f;

    private void Start()
    {
        if (GorillaLocomotion.Player.Instance == null)
        {
            Debug.LogError("Player not found dude are u dumb");
        }
    }

    private void Update()
    {
        HandleRoundStart();
        UpdateTagCooldown();
    }

    private void HandleRoundStart()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= minPlayersToStart && !IsRoundOngoing())
        {
            StartCoroutine(StartRound());
        }
    }

    private void UpdateTagCooldown()
    {
        if (tagCooldown > 0)
        {
            tagCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tagCooldown <= 0 && other.CompareTag("Player") && isTagged)
        {
            AttemptToTagPlayer(other);
        }
    }

    private void AttemptToTagPlayer(Collider other)
    {
        GorillaLocomotion.Player otherPlayer = other.GetComponent<GorillaLocomotion.Player>();
        if (otherPlayer != null && !otherPlayer.disableMovement)
        {
            PhotonView otherPhotonView = otherPlayer.GetComponent<PhotonView>();
            if (otherPhotonView != null)
            {
                otherPhotonView.RPC("TagPlayer", RpcTarget.All, otherPhotonView.ViewID);
            }
        }
    }

    [PunRPC]
    private void TagPlayer(int viewID)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        if (targetView != null)
        {
            GorillaLocomotion.Player targetPlayer = targetView.GetComponent<GorillaLocomotion.Player>();
            if (targetPlayer != null)
            {
                StartCoroutine(HandleTag(targetPlayer));
            }
        }
    }

    private IEnumerator HandleTag(GorillaLocomotion.Player targetPlayer)
    {
        photonView.RPC("PlayTagSound", RpcTarget.All);
        targetPlayer.disableMovement = true;

        ChangePlayerMaterial(targetPlayer, true);
        isTagged = true;

        yield return new WaitForSeconds(tagTime);

        targetPlayer.disableMovement = false;
        ChangePlayerMaterial(targetPlayer, false);
        isTagged = false;
    }

    private void ChangePlayerMaterial(GorillaLocomotion.Player targetPlayer, bool tagged)
    {
        PhotonView targetPhotonView = targetPlayer.GetComponent<PhotonView>();
        if (targetPhotonView != null)
        {
            targetPhotonView.RPC("ChangeMaterial", RpcTarget.All, targetPhotonView.ViewID, tagged);
        }
    }

    private IEnumerator StartRound()
    {
        List<GorillaLocomotion.Player> players = GetAllPlayersInRoom();

        if (players.Count > 0)
        {
            GorillaLocomotion.Player tagger = players[Random.Range(0, players.Count)];
            TagSelectedPlayer(tagger);

            yield return new WaitForSeconds(tagTime);

            photonView.RPC("PlayEndRoundSound", RpcTarget.All);
            DisableAllPlayersMovement(players);

            yield return new WaitForSeconds(endRoundTime);

            GorillaLocomotion.Player newTagger = players[Random.Range(0, players.Count)];
            ResetPlayersState(players, newTagger);
        }
    }

    private List<GorillaLocomotion.Player> GetAllPlayersInRoom()
    {
        List<GorillaLocomotion.Player> players = new List<GorillaLocomotion.Player>();
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            PhotonView view = PhotonView.Find(player.ActorNumber);
            if (view != null)
            {
                GorillaLocomotion.Player script = view.GetComponent<GorillaLocomotion.Player>();
                if (script != null)
                {
                    players.Add(script);
                }
            }
        }
        return players;
    }

    private void TagSelectedPlayer(GorillaLocomotion.Player tagger)
    {
        PhotonView taggerPhotonView = tagger.GetComponent<PhotonView>();
        if (taggerPhotonView != null)
        {
            taggerPhotonView.RPC("TagPlayer", RpcTarget.All, taggerPhotonView.ViewID);
        }
    }

    private void DisableAllPlayersMovement(List<GorillaLocomotion.Player> players)
    {
        foreach (GorillaLocomotion.Player player in players)
        {
            player.disableMovement = true;
            ChangePlayerMaterial(player, true);
        }
    }

    private void ResetPlayersState(List<GorillaLocomotion.Player> players, GorillaLocomotion.Player newTagger)
    {
        foreach (GorillaLocomotion.Player player in players)
        {
            player.disableMovement = false;
            ChangePlayerMaterial(player, player == newTagger);
            isTagged = player == newTagger;
        }
    }

    private bool IsRoundOngoing()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            PhotonView view = PhotonView.Find(player.ActorNumber);
            if (view != null)
            {
                GorillaLocomotion.Player script = view.GetComponent<GorillaLocomotion.Player>();
                if (script != null && script.disableMovement)
                {
                    return true;
                }
            }
        }
        return false;
    }

    [PunRPC]
    private void PlayTagSound()
    {
        tagSound.Play();
    }

    [PunRPC]
    private void PlayEndRoundSound()
    {
        endRoundSound.Play();
    }

    [PunRPC]
    private void ChangeMaterial(int viewID, bool isTagged)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        if (targetView != null)
        {
            GorillaLocomotion.Player targetPlayer = targetView.GetComponent<GorillaLocomotion.Player>();
            if (targetPlayer != null)
            {
                SkinnedMeshRenderer targetRenderer = targetPlayer.GetComponent<SkinnedMeshRenderer>();
                targetRenderer.material = isTagged ? taggedMaterial : untaggedMaterial;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isTagged);
            stream.SendNext(tagCooldown);
        }
        else
        {
            isTagged = (bool)stream.ReceiveNext();
            tagCooldown = (float)stream.ReceiveNext();
        }
    }
}
