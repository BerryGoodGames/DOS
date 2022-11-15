using System;
using System.Linq;
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

    public CardData(byte[] data)
    {
        // If the system architecture is little-endian (that is, little end first),
        // reverse the byte array.
        for(int i = 0; i < data.Length; i += 4)
        {
            byte[] bytes = data.Skip(i).Take(i + 4).ToArray();

            // if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytes);

            switch(i)
            {
                case 0:
                    type = BitConverter.ToInt32(bytes, 0);
                    break;
                case 4:
                    color = (CardColor)BitConverter.ToInt32(bytes, 0);
                    break;
                case 8:
                    location = BitConverter.ToInt32(bytes, 0);
                    break;
                case 12:
                    position = BitConverter.ToInt32(bytes, 0);
                    break;
            }
        }
    }

    #region (DE)SERIALIZE
    public static byte[] Serialize(object obj)
    {
        CardData data = (CardData)obj;

        int[] intArray = { data.type, (int)data.color, data.location, data.position };

        byte[] result = new byte[intArray.Length * sizeof(int)];
        Buffer.BlockCopy(intArray, 0, result, 0, result.Length);

        return result;
    }

    public static object Deserialize(byte[] data)
    {
        return new CardData(data);
    }
    #endregion

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
