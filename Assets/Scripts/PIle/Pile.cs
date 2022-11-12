using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public int id;
    public Stack<CardData> cardStack;
    private bool showTopSide;

    public void Shuffle()
    {
        
    }
    public void Transfer(Pile other)
    {

    }
    public void Clear()
    {

    }
    public void Init()
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
