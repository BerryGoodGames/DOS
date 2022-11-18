using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Reflection;
using ExitGames.Client.Photon;

public class MCard : MonoBehaviourPun
{
    #region VARIABLES
    // singleton
    public static MCard Instance { get; private set; }

    // editor
    [Header("References")]
    [SerializeField] private CPile DrawPile;

    // non-editor
    [HideInInspector] public List<CardData> cardList;
    public static readonly Type[] cardTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(CCard)).ToArray();
    public static CCard[] existingCards;
    #endregion


    private void Start()
    {
        existingCards = new CCard[] {
           MPrefab.Instance.NumberCard.GetComponent<CCard>(),
           MPrefab.Instance.ReverseCard.GetComponent<CCard>(),
           MPrefab.Instance.SkipCard.GetComponent<CCard>(),
           MPrefab.Instance.SwapCard.GetComponent<CCard>(),
           MPrefab.Instance.PlusOneGlobalCard.GetComponent<CCard>(),
           MPrefab.Instance.PlusTwoCard.GetComponent<CCard>(),
           MPrefab.Instance.WildCard.GetComponent<CCard>(),
           MPrefab.Instance.WildPlusFourCard.GetComponent<CCard>(),
       };

        InitializeCards();
    }

    public void InitializeCards()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int position = 0;
        foreach (CCard eCard in existingCards)
        {
            foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                for (int i = 0; i < eCard.AmountPerColor; i++)
                {
                    int j = eCard.CardTypeEnum == CardType.NUMBER ? 0 : 9;

                    for (; j <= 9; j++)
                    {
                        if (eCard.CardTypeEnum == CardType.NUMBER && i == 1 && j == 0) continue;
                        CardData data = new(eCard.CardTypeEnum, j, color, 0, position);
                        Instance.DrawPile.cardStack.Push(data);
                        position++;
                    }
                }
            }
        }

        DrawPile.Shuffle();

        foreach (CardData data in DrawPile.cardStack.ToArray())
        {
            print(data.GetCardType());
        }

        print("//////////////");

        photonView.RPC("SyncDrawPile", RpcTarget.All, DrawPile.cardStack.ToArray());
    }

    [PunRPC]
    public void SyncDrawPile(CardData[] drawStack)
    {
        Instance.DrawPile.cardStack = new(drawStack.Reverse());
        foreach (CardData data in Instance.DrawPile.cardStack)
            print(data.GetCardType());
    }

    public static CCard CreateCard(CardType cardType, int cardNumber, CardColor color, int location, int position)
    {
        MethodInfo method = typeof(MCard).GetMethod(nameof(MCard.CreateCardGeneric));
        MethodInfo generic = method.MakeGenericMethod(cardType.GetCardType());
        return (CCard)generic.Invoke(null, new object[] { cardType == CardType.NUMBER ? cardNumber : (int)cardType + 10, color, location, position });
    }

    public static T CreateCardGeneric<T>(int type, CardColor color, int location, int position) where T : CCard, new()
    {
        // creates Card and sets parameters
        T card = new();

        if (card.CardTypeEnum != CardType.NUMBER) type = (int)card.CardTypeEnum + 10;

        card.data = new(type, color, location, position);
        return card;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        PhotonPeer.RegisterType(typeof(CardData), (byte)'C', CardData.Serialize, CardData.Deserialize);
    }
}
