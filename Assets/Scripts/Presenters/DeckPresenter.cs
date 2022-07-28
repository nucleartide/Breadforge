using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPresenter : MonoBehaviour
{
    public Deck Deck;

    void Start()
    {
        Debug.Log("this is the deck presenter");
    }

    /*
    public CardStatistics Draw()
    {
        var lastCard = InPlayCards[InPlayCards.Count - 1];
        InPlayCards.RemoveAt(InPlayCards.Count - 1);
        return lastCard;
    }

    public void Shuffle()
    {
        throw new System.NotImplementedException();
    }
    */
}
