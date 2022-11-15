using System;
using UnityEngine;

/// <summary>
/// Represents one card absolutely
/// </summary>
[Serializable]
public class CardData
{
    public int type;
    public CardColor color;
    public int location;
    public int position;

    public CardData(int type, CardColor color, int location, int position)
    {
        this.type = type;
        this.color = color;
        this.location = location;
        this.position = position;
    }

    public CardData(CardType type, int cardNumber, CardColor color, int location, int position)
    {
        this.type = type == CardType.NUMBER? cardNumber : (int) type + 10;
        this.color = color;
        this.location = location;
        this.position = position;
    }

    public CardType GetCardType()
    {
        return type <= 9 ? CardType.NUMBER : (CardType)(type - 10);
    }

    public CPlayer GetPlayer()
    {
        return !IsOnPlayer() ? null : MPlayer.GetPlayerById(location);
    }
    public bool IsOnPlayer()
    {
        return location > 1;
    }

    public int GetIndex()
    {
        return MCard.Instance.cardList.IndexOf(this);
    }

    public static CardData TestCard()
    {
        return new CardData(4, CardColor.BLUE, 0, 0);
    }

    public static void UpdateCardDatasAllClients()
    {
        MGame.Instance.photonView.RPC("ReceiveCardDatas", Photon.Pun.RpcTarget.Others, MCard.Instance.cardList.ToArray());
    }

    public CardData Clone()
    {
        return new(type, color, location, position);
    }
}
