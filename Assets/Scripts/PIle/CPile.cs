using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CPile : MonoBehaviour
{
    public int id;
    public Stack<CardData> cardStack = new();
    public bool showTopSide;

    public void Shuffle()
    {
        CardData[] cardStackArray = cardStack.ToArray();

        // switches each card with random index
        for(int i = 0; i < cardStackArray.Length; i++)
        {
            int switchIndex = Random.Range(0, cardStackArray.Length - 1);
            if (switchIndex == i) continue;
            CardData switchTarget = cardStackArray[switchIndex].Clone();
            cardStackArray[switchIndex] = cardStackArray[i];
            cardStackArray[i] = switchTarget;
        }

        cardStack = new(cardStackArray);
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
