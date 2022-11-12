using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCard : MonoBehaviour
{
    public static MCard Instance { get; private set; }

    [HideInInspector] public List<CardData> cardList;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
