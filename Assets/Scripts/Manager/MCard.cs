using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MCard : MonoBehaviour
{
    public static MCard Instance { get; private set; }

    [HideInInspector] public List<CardData> cardList;

    public static void InitializeCards()
    {
        
    }

    public static T CreateCard<T>(CardColor color, int location, int position) where T : CCard, new()
    {
        // creates Card and sets parameters
        T card = new();

        card.data = new((int)card.cardType, color, location, position);
        return card;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
