using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour
{
    [HideInInspector] public int id;
    [HideInInspector] public Player photonPlayer;
    private string nickName;
    private List<CardData> currentCards;
    
    public void FetchCards(List<CardData> data)
    {
        currentCards.Clear();
        foreach(CardData card in data)
        {
            if(card.GetPlayer() == this) currentCards.Add(card);
        }
    }
    public void FetchCards()
    {
        FetchCards(MCard.Instance.cardList);
    }

    public void TakeCard(CPile pile)
    {
        // take card from pile to current player hand
        int topCardIndex = pile.cardStack.Pop().GetIndex();
        CardData cardInGlobalArr = MCard.Instance.cardList[topCardIndex];

        currentCards.Add(cardInGlobalArr);
        cardInGlobalArr.location = id;
        cardInGlobalArr.position = currentCards.Count - 1;
    }
    public void PlaceCard(CardData card, CPile pile)
    {
        // place card to pile
        int cardIndex = card.GetIndex();
        CardData globalCard = MCard.Instance.cardList[cardIndex];

        pile.cardStack.Push(globalCard);
        currentCards.Remove(globalCard);
        globalCard.location = pile.id;
        globalCard.position = pile.GetCardIndex(card);
    }
}
