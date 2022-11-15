using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGame : MonoBehaviour
{
    public static MGame Instance { get; private set; }

    public static CCard[] existingCards = { new CCardNumber(), new CCardPlusTwo(), new CCardPlusOneGlobal(), new CCardWild(), new CCardWildPlusFour(), new CCardReverse(), new CCardSwap(), new CCardSkip() };

    public PhotonView photonView;

    [PunRPC]
    public void ReceiveCardDatas(CardData[] data)
    {
        MCard.Instance.cardList.Clear();
        foreach(CardData c in data)
        {
            MCard.Instance.cardList.Add(c);
        }

        // update player cards
        MPlayer.FetchCardsAll();

        GameStateChanged();
    }


    public static int GameStateChanged()
    {
        return 1;
    }

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
}
