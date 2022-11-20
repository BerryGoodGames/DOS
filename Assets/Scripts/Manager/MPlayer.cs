using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MPlayer : MonoBehaviour
{
    public static MPlayer Instance { get; private set; }

    [HideInInspector] public List<CPlayer> playerList;
    public Dictionary<Player, int> playerToId = new();

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

            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                // Instantiate player
                CPlayer playerController = Instantiate(MPrefab.Instance.Player, MGame.Instance.playerContainer).GetComponent<CPlayer>();

                // generate id for player
                int index = UnityEngine.Random.Range(0, avaiableIds.Count);
                playerController.id = avaiableIds[index];
                avaiableIds.RemoveAt(index);

                // add photonPlayer to player
                playerController.photonPlayer = PhotonNetwork.LocalPlayer.Get(i);
                playerToId.Add(playerController.photonPlayer, playerController.id);
            }
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
    public void SyncAllPlayers(Dictionary<Player, int> playerInfo)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            // Instantiate player
            CPlayer playerController = Instantiate(MPrefab.Instance.Player, MGame.Instance.playerContainer).GetComponent<CPlayer>();

            // set id for player
            playerController.id = playerInfo[player];

            // add photonPlayer to player
            playerController.photonPlayer = player;
            playerToId.Add(playerController.photonPlayer, playerController.id);
        }
    }
}
