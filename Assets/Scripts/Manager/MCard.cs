using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Reflection;

public class MCard : MonoBehaviour
{
    #region VARIABLES
    // singleton
    public static MCard Instance { get; private set; }

    // editor
    [Header("References")]
    [SerializeField] private CPile DrawPile;

    // non-editor
    [HideInInspector] public List<CardData> cardList;
    public static readonly Type[] cardTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(CCard)).ToArray();
    public static CCard[] existingCards = { new CCardNumber(), new CCardPlusTwo(), new CCardPlusOneGlobal(), new CCardWild(), new CCardWildPlusFour(), new CCardReverse(), new CCardSwap(), new CCardSkip() };
    #endregion


    private void Start()
    {
        InitializeCards();

        print(existingCards.Length);
    }

    public static void InitializeCards()
    {
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
                        CardData data = CreateCard(eCard.CardTypeEnum, j, color, 0, position).data;
                        Instance.DrawPile.cardStack.Push(data);
                        position++;
                    }
                }
            }
        }

        Instance.DrawPile.Shuffle();
    }

    public static T CreateCardGeneric<T>(int type, CardColor color, int location, int position) where T : CCard, new()
    {
        // creates Card and sets parameters


        T card = new();

        if (card.CardTypeEnum != CardType.NUMBER) type = (int)card.CardTypeEnum + 10;

        card.data = new(type, color, location, position);
        card.data = new((int)card.CardTypeEnum, color, location, position);
        card.data = new((int)card.CardTypeEnum, color, location, position);
        return card;
    }

    public static CCard CreateCard(CardType cardType, int cardNumber, CardColor color, int location, int position)
    {
        MethodInfo method = typeof(MCard).GetMethod(nameof(MCard.CreateCardGeneric));
        MethodInfo generic = method.MakeGenericMethod(cardType.GetCardType());
        return (CCard)generic.Invoke(null, new object[] { cardType == CardType.NUMBER ? cardNumber : (int)cardType + 10, color, location, position });
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
