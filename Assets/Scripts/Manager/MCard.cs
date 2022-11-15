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

    public static void InitializeCards()
    {
        int position = 0;
        foreach(CCard eCard in existingCards)
        {
            foreach(CardColor color in Enum.GetValues(typeof(CardColor)))
            {
                for(int i = 0; i < eCard.AmountPerColor; i++)
                {
                    int j = eCard.cardType == CardType.NUMBER ? 0 : 9 ;

                    for(; j <= 9; j++)
                    {
                        if (eCard.cardType == CardType.NUMBER && i == 1 && j == 0) continue;
                        CardData data = new(eCard.cardType, j, color, 0, position);
                        Instance.DrawPile.cardStack.Push(data);
                        position++;
                    }
                }
            }
        }

        Instance.DrawPile.Shuffle();
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
