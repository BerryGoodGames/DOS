using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MCard : MonoBehaviour
{
    public static MCard Instance { get; private set; }

    [HideInInspector] public List<CardData> cardList;


    public static void InitializeCards()
    {
        
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
