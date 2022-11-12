using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayer : MonoBehaviour
{
    public static MPlayer Instance { get; private set; }

    [HideInInspector] public List<CPlayer> playerList;

    public static CPlayer GetPlayerById(int id)
    {
        foreach(CPlayer player in Instance.playerList)
        {
            if (player.id == id) return player;
        }
        return null;
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
}
