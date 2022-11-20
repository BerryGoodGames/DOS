using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExitGames.Client.Photon;

public class MPlayer : MonoBehaviourPun
{
    public static MPlayer Instance { get; private set; }

    [HideInInspector] public List<CPlayer> playerList = new();
    public Dictionary<Player, int> playerToId = new();
    private Dictionary<int, int> photonIdToId = new();

    public static CPlayer GetPlayerById(int id)
    {
        foreach(CPlayer player in Instance.playerList)
        {
            if (player.id == id) return player;
        }
        return null;
    }

    private void InitializePlayers()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            // generate avaible id's
            List<int> avaiableIds = Enumerable.Range(CPile.discardPileId + 1, PhotonNetwork.CurrentRoom.PlayerCount).ToList();

            foreach (Player player in MLobby.photonPlayerList)
            {
                // Instantiate player
                CPlayer playerController = Instantiate(MPrefab.Instance.Player, MGame.Instance.playerContainer).GetComponent<CPlayer>();
                playerList.Add(playerController);

                // generate id for player
                int index = UnityEngine.Random.Range(0, avaiableIds.Count);
                playerController.id = avaiableIds[index];
                avaiableIds.RemoveAt(index);

                // add photonPlayer to player
                playerController.photonPlayer = player;
                print(playerController.photonPlayer == null);
                playerToId.Add(playerController.photonPlayer, playerController.id);
                photonIdToId.Add(playerController.photonPlayer.ActorNumber, playerController.id);
            }

            photonView.RPC("SyncAllPlayers", RpcTarget.Others, photonIdToId);
        }
    }

    public static void FetchCardsAll()
    {
        // lets every player fetch their cards
        foreach(CPlayer cPlayer in Instance.playerList)
        {
            cPlayer.FetchCards();
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        InitializePlayers();
    }

    [PunRPC]
    public void SyncAllPlayers(Dictionary<int, int> playerInfo)
    {
        foreach (Player player in MLobby.photonPlayerList)
        {
            // Instantiate player
            CPlayer playerController = Instantiate(MPrefab.Instance.Player, MGame.Instance.playerContainer).GetComponent<CPlayer>();
            playerList.Add(playerController);

            // set id for player
            playerController.id = playerInfo[player.ActorNumber];

            // add photonPlayer to player
            playerController.photonPlayer = player;
            playerToId.Add(playerController.photonPlayer, playerController.id);
        }
    }
}
