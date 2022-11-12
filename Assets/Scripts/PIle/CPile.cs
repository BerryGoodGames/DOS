using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CPile : MonoBehaviour
{
    [HideInInspector] public int id;
    public Stack<CardData> cardStack;
    public bool showTopSide;

    public void Shuffle()
    {
        
    }
    public void Transfer(CPile other)
    {

    }

    public int GetCardIndex(CardData data)
    {
        int index = 0;
        foreach(CardData card in cardStack)
        {
            if (card == data) return index;
            index++;
        }
        return -1;
    }
}
